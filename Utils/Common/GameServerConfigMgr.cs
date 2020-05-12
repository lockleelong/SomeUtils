using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameServerConfigMgr : Singleton<GameServerConfigMgr>
{
    public enum ConfigCode
    {
        /// <summary>
        /// ��Ϸ�ؿ�����
        /// </summary>
        GameLevelMenu,
        /// <summary>
        /// ��Ϸ�ؿ��޵�
        /// </summary>
        GameLevelSuperman
    }

    #region Get �������ñ����ȡ������Ϣ
    /// <summary>
    /// �������ñ����ȡ������Ϣ
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameServerConfigEntity Get(ConfigCode code)
    {
        GameServerConfigEntity entity = null;
        if (m_List == null || m_List.Count == 0)
        {
            entity = new GameServerConfigEntity() { ConfigCode = code.ToString() };
            m_List.Add(entity);
        }

        for (int i = 0; i < m_List.Count; i++)
        {
            if (m_List[i].ConfigCode.Equals(code.ToString(), System.StringComparison.CurrentCultureIgnoreCase))
            {
                entity = m_List[i];
                break;
            }
        }

        if (entity == null)
        {
            entity = new GameServerConfigEntity() { ConfigCode = code.ToString() };
            m_List.Add(entity);
        }

        return entity;
    }
    #endregion

    #region AddConfig ��ӷ���������
    /// <summary>
    /// ��ӷ���������
    /// </summary>
    /// <param name="configCode"></param>
    /// <param name="isOpen"></param>
    /// <param name="param"></param>
    public void AddConfig(string configCode, bool isOpen, string param)
    {
        m_List.Add(new GameServerConfigEntity() { ConfigCode = configCode, IsOpen = isOpen, Param = param });
    }
    #endregion

    private List<GameServerConfigEntity> m_List = new List<GameServerConfigEntity>();

    public class GameServerConfigEntity
    {
        public string ConfigCode; //���ñ���
        public bool IsOpen; //�Ƿ���
        public string Param; //���ò���
    }
}