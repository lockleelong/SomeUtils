//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-02-05 11:58:55
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[XLua.LuaCallCSharp]
public class RetValue
{
    public bool HasError;

    public string ErrorMsg;

    public object Value;
}