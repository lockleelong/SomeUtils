
//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-09-19 22:05:20
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Language数据管理
/// </summary>
public partial class LanguageDBModel : AbstractDBModel<LanguageDBModel, LanguageEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "Language.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override LanguageEntity MakeEntity(GameDataTableParser parse)
    {
        LanguageEntity entity = new LanguageEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Module = parse.GetFieldValue("Module");
        entity.Key = parse.GetFieldValue("Key");
        entity.Desc = parse.GetFieldValue("Desc");
        entity.CN = parse.GetFieldValue("CN");
        entity.EN = parse.GetFieldValue("EN");
        return entity;
    }
}
