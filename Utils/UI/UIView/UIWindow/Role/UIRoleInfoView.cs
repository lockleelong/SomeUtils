//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-07-12 21:50:44
//备    注：
//===================================================
using UnityEngine;
using System.Collections;

public class UIRoleInfoView : UIWindowViewBase
{
    /// <summary>
    /// 角色装备View
    /// </summary>
    [SerializeField]
    private UIRoleEquipView m_UIRoleEquipView;

    /// <summary>
    /// 角色详情视图
    /// </summary>
    [SerializeField]
    private UIRoleInfoDetailView m_UIRoleInfoDetailView;

    /// <summary>
    /// 
    /// </summary>
    public void SetRoleInfo(TransferData data)
    {
        m_UIRoleEquipView.SetUI(data);
        m_UIRoleInfoDetailView.SetUI(data);
    }

    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);
    }

    protected override void BeforeOnDestroy()
    {
        m_UIRoleEquipView = null;
        m_UIRoleInfoDetailView = null;
    }
}