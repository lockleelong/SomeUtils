using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// 场景管理器
/// </summary>
public class SceneMgr : Singleton<SceneMgr>
{
    public SceneMgr()
    {
        //服务器返回角色进入世界地图场景消息
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDef.WorldMap_RoleEnterReturn, OnWorldMapRoleEnterReturn);
    }

    /// <summary>
    /// 当前玩法类型
    /// </summary>
    public PlayType CurrPlayType
    {
        get;
        private set;
    }

    /// <summary>
    /// 当前场景类型
    /// </summary>
    public SceneType CurrSceneType
    {
        get;
        private set;
    }

    /// <summary>
    /// 是否战斗场景（用于PVP）
    /// </summary>
    public bool IsFightingScene
    {
        get;
        private set;
    }

    /// <summary>
    /// 去登录场景
    /// </summary>
    public void LoadToLogOn()
    {
        CurrSceneType = SceneType.LogOn;
        SceneManager.LoadScene("Scene_Loading");
    }

    /// <summary>
    /// 去选择角色场景
    /// </summary>
    public void LoadToSelectRole()
    {
        AppDebug.Log("跳转到选人场景");
        CurrSceneType = SceneType.SelectRole;
        SceneManager.LoadScene("Scene_Loading");
    }

    #region LoadToWorldMap 去世界地图场景（主城+野外场景）

    private int m_CurrWorldMapId;

    /// <summary>
    /// 当前所在的世界地图编号
    /// </summary>
    public int CurrWorldMapId { get { return m_CurrWorldMapId; } }

    /// <summary>
    /// 目标世界地图的传送点Id
    /// </summary>
    public int TargertWorldMapTransPosId;

    private int m_WillToWorldMapId = 0;//想要去的世界地图场景Id

    /// <summary>
    /// 去世界地图场景（主城+野外场景）
    /// </summary>
    public void LoadToWorldMap(int worldMapId)
    {
        if (m_CurrWorldMapId == worldMapId)
        {
            //您已经在目标场景中
            MessageCtrl.Instance.ShowMessageView(LanguageDBModel.Instance.GetText(101001));
            return;
        }

        WorldMapRoleEnter(worldMapId);
    }

    /// <summary>
    /// 客户端发送进入世界地图消息
    /// </summary>
    /// <param name="worldMapId"></param>
    private void WorldMapRoleEnter(int worldMapId)
    {
        m_WillToWorldMapId = worldMapId;

        WorldMap_RoleEnterProto proto = new WorldMap_RoleEnterProto();
        proto.WorldMapSceneId = m_WillToWorldMapId;

        NetWorkSocket.Instance.SendMsg(proto.ToArray());
    }

    /// <summary>
    /// 服务器返回角色进入世界地图场景消息
    /// </summary>
    /// <param name="buffer"></param>
    private void OnWorldMapRoleEnterReturn(byte[] buffer)
    {
        WorldMap_RoleEnterReturnProto proto = WorldMap_RoleEnterReturnProto.GetProto(buffer);
        if (proto.IsSuccess)
        {
            m_CurrWorldMapId = m_WillToWorldMapId;

            CurrSceneType = SceneType.WorldMap;
            CurrPlayType = PlayType.PVP;

            WorldMapEntity entity = WorldMapDBModel.Instance.Get(m_CurrWorldMapId);
            if (entity != null)
            {
                //不是主城 就可以战斗
                IsFightingScene = entity.IsCity == 0;
            }

            SceneManager.LoadScene("Scene_Loading");
        }
    }

    #endregion


    #region 进入游戏关卡
    private int m_CurrGameLevelId;

    public int CurrGameLevelId { get { return m_CurrGameLevelId; } }

    private GameLevelGrade m_CurrGameLevelGrade;

    public GameLevelGrade CurrGameLevelGrade { get { return m_CurrGameLevelGrade; } }

    public void LoadToGameLevel(int gameLevelId, GameLevelGrade grade)
    {
        m_CurrWorldMapId = -1;

        m_CurrGameLevelId = gameLevelId;
        m_CurrGameLevelGrade = grade;

        CurrSceneType = SceneType.GameLevel;
        CurrPlayType = PlayType.PVE;

        SceneManager.LoadScene("Scene_Loading");
    }
    #endregion
}