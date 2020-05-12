//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-04-21 22:27:21
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System;

public class UISceneViewBase : UIViewBase
{
    /// <summary>
    /// 当前画布
    /// </summary>
    public Canvas CurrCanvas;

    /// <summary>
    /// 容器_居中
    /// </summary>
    [SerializeField]
    public Transform Container_Center;

    /// <summary>
    /// HUD
    /// </summary>
    public bl_HUDText HUDText;

    /// <summary>
    /// 加载完毕
    /// </summary>
    public Action<GameObject> OnLoadComplete;
}