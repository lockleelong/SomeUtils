using UnityEngine;
using System.Collections;

[XLua.LuaCallCSharp]
public class ChannelInitConfig
{
    /// <summary>
    /// �ʺŷ�����ʱ��
    /// </summary>
    public long ServerTime;

    /// <summary>
    /// ��Դ��ַ
    /// </summary>
    public string SourceUrl;

    /// <summary>
    /// ��ֵ�ص���ַ
    /// </summary>
    public string RechargeUrl;

    /// <summary>
    /// TDͳ���ʺ�
    /// </summary>
    public string TDAppId;

    /// <summary>
    /// �Ƿ���TDͳ��
    /// </summary>
    public bool IsOpenTD;

    /// <summary>
    /// ��ֵ������ʶ����
    /// </summary>
    public int PayServerNo;
}