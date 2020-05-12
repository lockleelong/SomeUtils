//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-01 22:26:02
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using System.Xml.Linq;
using XLua;

#if UNITY_IPHONE
using UnityEngine.iOS;
#endif

using System.Collections.Generic;

[XLua.LuaCallCSharp]
public class GlobalInit : MonoBehaviour
{
    public delegate void OnReceiveProtoHandler(ushort protoCode, byte[] buffer);

    //定义委托
    public OnReceiveProtoHandler OnReceiveProto;


    #region 常量
    /// <summary>
    /// 昵称KEY
    /// </summary>
    public const string MMO_NICKNAME = "MMO_NICKNAME";

    /// <summary>
    /// 密码KEY
    /// </summary>
    public const string MMO_PWD = "MMO_PWD";

    /// <summary>
    /// 账户服务器地址
    /// </summary>
    public static string WebAccountUrl;

    /// <summary>
    /// 渠道号
    /// </summary>
    public static int ChannelId;

    /// <summary>
    /// 内部版本号
    /// </summary>
    public static int InnerVersion;

    #endregion

    /// <summary>
    /// 渠道初始化配置
    /// </summary>
    public ChannelInitConfig CurrChannelInitConfig;

    public static GlobalInit Instance;

    /// <summary>
    /// T4M
    /// </summary>
    public Shader T4MShader;

    /// <summary>
    /// 天空的Shader
    /// </summary>
    public Shader MogoSkyboxShader;

    /// <summary>
    /// 主角色镜像
    /// </summary>
    [HideInInspector]
    public Dictionary<int, GameObject> JobObjectDic = new Dictionary<int, GameObject>();

    /// <summary>
    /// 主角信息
    /// </summary>
    [HideInInspector]
    public RoleInfoMainPlayer MainPlayerInfo;

    //==========================================

    /// <summary>
    /// 玩家注册时候的昵称
    /// </summary>
    [HideInInspector]
    public string CurrRoleNickName;

    /// <summary>
    /// 当前玩家
    /// </summary>
    [HideInInspector]
    public RoleCtrl CurrPlayer;

    /// <summary>
    /// UI动画曲线
    /// </summary>
    public AnimationCurve UIAnimationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));

    /// <summary>
    /// 当前帐号
    /// </summary>
    [HideInInspector]
    public RetAccountEntity CurrAccount;

    /// <summary>
    /// 当前选择的区服
    /// </summary>
    [HideInInspector]
    public RetGameServerEntity CurrSelectGameServer;

    /// <summary>
    /// 获取当前年月日十分秒毫秒格式
    /// </summary>
    /// <returns></returns>
    public string GetCurrTime()
    {
        return DateTime.Now.ToString("yyyyMMddHHmmssfff");
    }

    /// <summary>
    /// 当前服务器时间
    /// </summary>
    [HideInInspector]
    public long CurrServerTime
    {
        get
        {
            if (CurrChannelInitConfig == null)
            {
                return (long)Time.unscaledTime;
            }
            else
            {
                return CurrChannelInitConfig.ServerTime + (long)Time.unscaledTime;
            }
        }
    }

    /// <summary>
    /// PING值(毫秒)
    /// </summary>
    [HideInInspector]
    public int PingValue;

    /// <summary>
    /// 游戏服务器的时间
    /// </summary>
    [HideInInspector]
    public long GameServerTime;

    /// <summary>
    /// 和服务器对表的时刻
    /// </summary>
    [HideInInspector]
    public float CheckServerTime;

    /// <summary>
    /// 获取当前的服务器时间
    /// </summary>
    /// <returns></returns>
    public long GetCurrServerTime()
    {
        return (int)((Time.realtimeSinceStartup - CheckServerTime) * 1000) + GameServerTime;
    }

    void Awake()
    {
        Application.targetFrameRate = 60;
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        CurrChannelInitConfig = new ChannelInitConfig();

        //初始化渠道配置
        InitChannelConfig(ref WebAccountUrl, ref ChannelId, ref InnerVersion);

        AppDebug.Log("WebAccountUrl=" + WebAccountUrl);
        AppDebug.Log("ChannelId=" + ChannelId);
        AppDebug.Log("InnerVersion=" + InnerVersion);

        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["ChannelId"] = ChannelId;
        dic["InnerVersion"] = InnerVersion;

        //初始化的时候 请求服务器时间
        NetWorkHttp.Instance.SendData(WebAccountUrl + "api/init", OnInitCallBack, isPost: true, dic: dic);

#if UNITY_STANDALONE_WIN
        string localFilePath = Application.persistentDataPath + "/";
        //AppDebug.Log(localFilePath);
        if (!Directory.Exists(localFilePath))
        {
            Directory.CreateDirectory(localFilePath);
        }
#endif
    }

    //初始化渠道配置文件
    void InitChannelConfig(ref string webAccountUrl, ref int channelId, ref int innerVersion)
    {
        TextAsset asset = Resources.Load("Config/ChannelConfig") as TextAsset;
        XDocument xDoc = XDocument.Parse(asset.text);
        XElement root = xDoc.Root;
        webAccountUrl = root.Element("WebAccountUrl").Attribute("Value").Value;
        channelId = root.Element("ChannelId").Attribute("Value").Value.ToInt();
        innerVersion = root.Element("InnerVersion").Attribute("Value").Value.ToInt();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && Input.GetKeyDown(KeyCode.F))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("已经清除本地存储");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            LuaHelper.Instance.LoadLuaView("RechargeCtrl");
        }

        //采集当前玩家坐标
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (GlobalInit.Instance.CurrPlayer == null) return;

            Transform trans = GlobalInit.Instance.CurrPlayer.gameObject.transform;
            string pos = string.Format("{0}_{1}_{2}_{3}", trans.position.x, trans.position.y, trans.position.z, trans.rotation.eulerAngles.y);
            Debug.Log("位置信息=" + pos);
        }
    }

    private void OnInitCallBack(NetWorkHttp.CallBackArgs obj)
    {
        if (!obj.HasError)
        {
            string item = obj.Value;
            LitJson.JsonData data = LitJson.JsonMapper.ToObject(obj.Value);

            bool hasError = (bool)data["HasError"];
            if (!hasError)
            {
                LitJson.JsonData config = LitJson.JsonMapper.ToObject(data["Value"].ToString());

                Debug.Log("config==" + data["Value"].ToString());

                CurrChannelInitConfig.ServerTime = long.Parse(config["ServerTime"].ToString());
                CurrChannelInitConfig.SourceUrl = config["SourceUrl"].ToString();
                CurrChannelInitConfig.RechargeUrl = config["RechargeUrl"].ToString();
                CurrChannelInitConfig.TDAppId = config["TDAppId"].ToString();
                CurrChannelInitConfig.IsOpenTD = int.Parse(config["IsOpenTD"].ToString()) == 1;

                Debug.Log("ServerTime==" + CurrChannelInitConfig.ServerTime);
                Debug.Log("SourceUrl==" + CurrChannelInitConfig.SourceUrl);

                if (DelegateDefine.Instance.ChannelInitOK != null)
                {
                    DelegateDefine.Instance.ChannelInitOK();
                }
            }
            else
            {
                string msg = data["ErrorMsg"].ToString();
                MessageCtrl.Instance.ShowMessageView(msg);
            }
        }
    }
}