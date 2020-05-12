using UnityEngine;
using System.Collections;
using PathologicalGames;
using System.Collections.Generic;

public class EffectMgr : Singleton<EffectMgr>
{
    /// <summary>
    /// 特效池
    /// </summary>
    private SpawnPool m_EffectPool;

    private MonoBehaviour m_Mono;

    private Dictionary<string, Transform> m_EffectDic = new Dictionary<string, Transform>();

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(MonoBehaviour mono)
    {
        m_Mono = mono;
        m_EffectPool = PoolManager.Pools.Create("Effect");
    }

    /// <summary>
    /// 播放特效
    /// </summary>
    /// <param name="effectName"></param>
    public void PlayEffect(string effectPath, string effectName, System.Action<Transform> onComplete)
    {
        if (m_EffectPool == null) return;
        if (!m_EffectDic.ContainsKey(effectName))
        {
            AssetBundleMgr.Instance.LoadOrDownload(effectPath + ".assetbundle", effectName,
                (GameObject obj) =>
                {
                    if (!m_EffectDic.ContainsKey(effectName))
                    {
                        //如果特效没有播放过
                        m_EffectDic[effectName] = obj.transform;

                        PrefabPool prefabPool = new PrefabPool(m_EffectDic[effectName]);
                        prefabPool.preloadAmount = 0; //预加载数量

                        prefabPool.cullDespawned = true; //是否开启缓存池自动清理模式
                        prefabPool.cullAbove = 5;// 缓存池自动清理 但是始终保留几个对象不清理
                        prefabPool.cullDelay = 2;//多长时间清理一次 单位是秒
                        prefabPool.cullMaxPerPass = 2; //每次清理几个

                        m_EffectPool.CreatePrefabPool(prefabPool);

                        if (onComplete != null)
                        {
                            onComplete(m_EffectPool.Spawn(m_EffectDic[effectName]));
                        }
                    }
                    else
                    {
                        if (onComplete != null)
                        {
                            onComplete(m_EffectPool.Spawn(m_EffectDic[effectName]));
                        }
                    }
                });
        }
        else
        {
            if (onComplete != null)
            {
                onComplete(m_EffectPool.Spawn(m_EffectDic[effectName]));
            }
        }
    }

    /// <summary>
    /// 销毁特效
    /// </summary>
    /// <param name="effect"></param>
    /// <param name="delay">延迟时间</param>
    public void DestroyEffect(Transform effect, float delay)
    {
        m_Mono.StartCoroutine(DestroyEffectCoroutine(effect, delay));
    }

    private IEnumerator DestroyEffectCoroutine(Transform effect, float delay)
    {
        yield return new WaitForSeconds(delay);
        m_EffectPool.Despawn(effect);
    }

    /// <summary>
    /// 清空
    /// </summary>
    public void Clear()
    {
        m_EffectDic.Clear();
        m_EffectPool = null;
    }
}