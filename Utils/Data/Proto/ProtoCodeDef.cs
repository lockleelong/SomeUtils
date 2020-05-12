//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-10-30 22:13:29
//备    注：
//===================================================
using System.Collections;

/// <summary>
/// 协议编号定义
/// </summary>
public class ProtoCodeDef
{
    /// <summary>
    /// 客户端发送登录区服消息
    /// </summary>
    public const ushort RoleOperation_LogOnGameServer = 10001;

    /// <summary>
    /// 服务器返回登录信息
    /// </summary>
    public const ushort RoleOperation_LogOnGameServerReturn = 10002;

    /// <summary>
    /// 客户端发送创建角色消息
    /// </summary>
    public const ushort RoleOperation_CreateRole = 10003;

    /// <summary>
    /// 服务器返回创建角色消息
    /// </summary>
    public const ushort RoleOperation_CreateRoleReturn = 10004;

    /// <summary>
    /// 客户端发送删除角色消息
    /// </summary>
    public const ushort RoleOperation_DeleteRole = 10005;

    /// <summary>
    /// 服务器返回删除角色消息
    /// </summary>
    public const ushort RoleOperation_DeleteRoleReturn = 10006;

    /// <summary>
    /// 客户端发送进入游戏消息
    /// </summary>
    public const ushort RoleOperation_EnterGame = 10007;

    /// <summary>
    /// 服务器返回进入游戏消息
    /// </summary>
    public const ushort RoleOperation_EnterGameReturn = 10008;

    /// <summary>
    /// 客户端查询角色信息
    /// </summary>
    public const ushort RoleOperation_SelectRoleInfo = 10009;

    /// <summary>
    /// 服务器返回角色信息
    /// </summary>
    public const ushort RoleOperation_SelectRoleInfoReturn = 10010;

    /// <summary>
    /// 服务器返回角色学会的技能
    /// </summary>
    public const ushort RoleData_SkillReturn = 11001;

    /// <summary>
    /// 客户端发送进入游戏关卡消息
    /// </summary>
    public const ushort GameLevel_Enter = 12001;

    /// <summary>
    /// 服务器返回进入关卡消息
    /// </summary>
    public const ushort GameLevel_EnterReturn = 12002;

    /// <summary>
    /// 客户端发送战斗胜利消息
    /// </summary>
    public const ushort GameLevel_Victory = 12003;

    /// <summary>
    /// 服务器返回战斗胜利消息
    /// </summary>
    public const ushort GameLevel_VictoryReturn = 12004;

    /// <summary>
    /// 客户端发送战斗失败消息
    /// </summary>
    public const ushort GameLevel_Fail = 12005;

    /// <summary>
    /// 服务器返回战斗失败消息
    /// </summary>
    public const ushort GameLevel_FailReturn = 12006;

    /// <summary>
    /// 客户端发送复活消息
    /// </summary>
    public const ushort GameLevel_Resurgence = 12007;

    /// <summary>
    /// 服务器返回复活消息
    /// </summary>
    public const ushort GameLevel_ResurgenceReturn = 12008;

}
