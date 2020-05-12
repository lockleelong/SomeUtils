//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-10-30 22:13:29
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 服务器返回进入游戏消息
/// </summary>
public struct RoleOperation_EnterGameReturnProto : IProto
{
    public ushort ProtoCode { get { return 10008; } }

    public bool IsSuccess; //是否成功
    public short MessageId; //错误编号

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteBool(IsSuccess);
            if(!IsSuccess)
            {
                ms.WriteShort(MessageId);
            }
            return ms.ToArray();
        }
    }

    public static RoleOperation_EnterGameReturnProto GetProto(byte[] buffer)
    {
        RoleOperation_EnterGameReturnProto proto = new RoleOperation_EnterGameReturnProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.IsSuccess = ms.ReadBool();
            if(!proto.IsSuccess)
            {
                proto.MessageId = ms.ReadShort();
            }
        }
        return proto;
    }
}
