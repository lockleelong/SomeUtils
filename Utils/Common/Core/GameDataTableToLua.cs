//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2017-06-08 20:43:06
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using XLua;

[LuaCallCSharp]
public class GameDataTableToLua 
{
    /// <summary>
    /// 表格数据（交叉数组）
    /// </summary>
    public string[][] Data;

    /// <summary>
    /// 行数
    /// </summary>
    public int Row;

    /// <summary>
    /// 列数
    /// </summary>
    public int Column;
}