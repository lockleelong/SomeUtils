//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-11-29 16:24:29
//备    注：场景UI控制器
//===================================================
using UnityEngine;
using System.Collections;

/// <summary>
/// 场景UI控制器
/// </summary>
public class UISceneCtrl : Singleton<UISceneCtrl>
{

    /// <summary>
    /// 场景UI类型
    /// </summary>
    public enum SceneUIType
    {
        /// <summary>
        /// 未定义
        /// </summary>
        None,
        /// <summary>
        /// 登录
        /// </summary>
        LogOn,
        /// <summary>
        /// 加载
        /// </summary>
        Loading,
        /// <summary>
        /// 选人场景
        /// </summary>
        SelectRole,
        /// <summary>
        /// 主城
        /// </summary>
        MainCity
    }

    /// <summary>
    /// 当前场景UI
    /// </summary>
    public UISceneViewBase CurrentUIScene;

    public void LoadSceneUI(string path, XLuaCustomExport.OnCreate OnCreate)
    {
        LoadSceneUI(SceneUIType.None, null, OnCreate, path);
    }

    #region LoadSceneUI 加载场景UI
    /// <summary>
    /// 加载场景UI
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public void LoadSceneUI(SceneUIType type, System.Action<GameObject> OnLoadComplete, XLuaCustomExport.OnCreate OnCreate = null, string path = null)
    {
        string strUIName = string.Empty;

        string newPath = string.Empty;

        if (type != SceneUIType.None)
        {
            switch (type)
            {
                case SceneUIType.LogOn:
                    strUIName = "UI_Root_LogOn";
                    break;
                case SceneUIType.SelectRole:
                    strUIName = "UI_Root_SelectRole";
                    break;
                case SceneUIType.Loading:
                    break;
                case SceneUIType.MainCity:
                    strUIName = "UI_Root_MainCity";
                    break;
            }

            newPath = string.Format("Download/Prefab/UIPrefab/UIScene/{0}.assetbundle", strUIName);
        }
        else
        {
            newPath = path;
        }

        AssetBundleMgr.Instance.LoadOrDownload(newPath, strUIName, (GameObject obj) =>
        {
            obj = Object.Instantiate(obj);
            CurrentUIScene = obj.GetComponent<UISceneViewBase>();

            if (OnLoadComplete != null)
            {
                OnLoadComplete(obj);
            }

            if (OnCreate != null)
            {
                //此时表示 是从Lua中加载的
                obj.GetOrCreatComponent<LuaViewBehaviour>();
                OnCreate(obj);
            }
        });
    }
    #endregion
}