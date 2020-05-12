//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-15 23:07:03
//备    注：角色管理器
//===================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoleMgr : Singleton<RoleMgr>
{
    #region InitMainPlayer 初始化主角
    /// <summary>
    /// 主角是否已经初始化
    /// </summary>
    private bool m_IsMainPlayerInit = false;

    /// <summary>
    /// 初始化主角
    /// </summary>
    public void InitMainPlayer()
    {
        if (m_IsMainPlayerInit) return;

        if (GlobalInit.Instance.MainPlayerInfo != null)
        {
            GameObject mainPlayerObj = Object.Instantiate(GlobalInit.Instance.JobObjectDic[GlobalInit.Instance.MainPlayerInfo.JobId]);
            Object.DontDestroyOnLoad(mainPlayerObj);


            GlobalInit.Instance.MainPlayerInfo.SetPhySkillId(JobDBModel.Instance.Get(GlobalInit.Instance.MainPlayerInfo.JobId).UsedPhyAttackIds);

            GlobalInit.Instance.CurrPlayer = mainPlayerObj.GetComponent<RoleCtrl>();
            GlobalInit.Instance.CurrPlayer.Init(RoleType.MainPlayer, GlobalInit.Instance.MainPlayerInfo, new RoleMainPlayerCityAI(GlobalInit.Instance.CurrPlayer));
        }

        m_IsMainPlayerInit = true;
    }
    #endregion

    #region LoadOtherRole 加载其他玩家
    /// <summary>
    /// 加载其他玩家
    /// </summary>
    /// <param name="roleId">角色编号</param>
    /// <param name="nickName">昵称</param>
    /// <param name="roleLevel">等级</param>
    /// <param name="jobId">职业编号</param>
    /// <param name="maxHP"></param>
    /// <param name="currHP"></param>
    /// <param name="maxMP"></param>
    /// <param name="currMP"></param>
    /// <returns></returns>
    public RoleCtrl LoadOtherRole(int roleId, string nickName, int roleLevel, int jobId, int maxHP, int currHP, int maxMP, int currMP)
    {
        GameObject roleObj = Object.Instantiate(GlobalInit.Instance.JobObjectDic[jobId]);
        RoleCtrl roleCtrl = roleObj.GetComponent<RoleCtrl>();

        //角色初始化的时候 要赋值基本信息
        //以后 包括 帮会信息 称号信息 等 也都要进行同步
        RoleInfoMainPlayer roleInfo = new RoleInfoMainPlayer();
        roleInfo.RoldId = roleId;
        roleInfo.RoleNickName = nickName;
        roleInfo.Level = roleLevel;
        roleInfo.JobId = (byte)roleId;
        roleInfo.MaxHP = maxHP;
        roleInfo.CurrHP = currHP;
        roleInfo.MaxMP = maxMP;
        roleInfo.CurrMP = currMP;

        roleCtrl.Init(RoleType.OTherRole, roleInfo, new OtherRoleAI(roleCtrl));

        return roleCtrl;
    }
    #endregion

    #region LoadHeadPic 加载角色头像
    /// <summary>
    /// 加载角色头像
    /// </summary>
    /// <param name="headPic"></param>
    /// <returns></returns>
    public Sprite LoadHeadPic(string headPic)
    {
        return Resources.Load(string.Format("UI/HeadImg/{0}", headPic), typeof(Sprite)) as Sprite;
    }
    #endregion

    #region LoadSkillPic 加载技能图标
    /// <summary>
    /// 加载技能图标
    /// </summary>
    /// <param name="skillPic"></param>
    /// <returns></returns>
    public Sprite LoadSkillPic(string skillPic)
    {
        return Resources.Load(string.Format("UI/SkillIco/{0}", skillPic), typeof(Sprite)) as Sprite;
    }
    #endregion

    #region LoadPlayer 根据玩家的职业编号 加载玩家
    /// <summary>
    /// 根据玩家的职业编号 加载玩家
    /// </summary>
    /// <param name="jobId"></param>
    /// <returns></returns>
    public GameObject LoadPlayer(int jobId)
    {
        if (GlobalInit.Instance.JobObjectDic.ContainsKey(jobId))
        {
            GameObject obj = GlobalInit.Instance.JobObjectDic[jobId];
            return Object.Instantiate(obj);
        }
        else
        {
            return null;
        }
    }
    #endregion

    #region LoadSprite 根据精灵编号返回精灵预设镜像
    /// <summary>
    /// 根据精灵编号返回精灵预设镜像
    /// </summary>
    /// <param name="spriteId"></param>
    /// <returns></returns>
    public void LoadSprite(int spriteId, System.Action<GameObject> onComplete)
    {
        SpriteEntity entity = SpriteDBModel.Instance.Get(spriteId);
        if (entity == null) return;

        AssetBundleMgr.Instance.LoadOrDownload(string.Format("Download/Prefab/RolePrefab/Monster/{0}.assetbundle", entity.PrefabName), entity.PrefabName, onComplete);
    }
    #endregion

    #region LoadNPC 加载NPC
    /// <summary>
    /// 加载NPC
    /// </summary>
    /// <param name="prefabName"></param>
    /// <returns></returns>
    public void LoadNPC(string prefabName, System.Action<GameObject> onComplete)
    {
        AssetBundleMgr.Instance.LoadOrDownload(string.Format("Download/Prefab/RolePrefab/NPC/{0}.assetbundle", prefabName), prefabName,
            (GameObject obj) =>
            {
                if (onComplete != null)
                {
                    onComplete(Object.Instantiate(obj));
                }
            });
    }
    #endregion

    public override void Dispose()
    {
        base.Dispose();
    }
}