using UnityEngine;
using System.Collections;

[XLua.LuaCallCSharp]
public class ChannelInitConfig
{
    /// <summary>
    /// 帐号服务器时间
    /// </summary>
    public long ServerTime;

    /// <summary>
    /// 资源地址
    /// </summary>
    public string SourceUrl;

    /// <summary>
    /// 充值回调地址
    /// </summary>
    public string RechargeUrl;

    /// <summary>
    /// TD统计帐号
    /// </summary>
    public string TDAppId;

    /// <summary>
    /// 是否开启TD统计
    /// </summary>
    public bool IsOpenTD;

    /// <summary>
    /// 充值服务器识别码
    /// </summary>
    public int PayServerNo;
}