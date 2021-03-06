﻿using UnityEngine;
[ExecuteInEditMode]
public class SerializedLightmapSetting : MonoBehaviour
{
    /// <summary>
    /// 灯光贴图
    /// </summary>
    public Texture2D[] lightmapFar, lightmapNear;

    /// <summary>
    /// 方式
    /// </summary>
    public LightmapsMode mode;

    public bool HasFog;
    public FogMode FogMode;
    public Color FogColor;
    public int StartDistance;
    public int EndDistance;

    void Start()
    {
        RenderSettings.fog = HasFog;
        RenderSettings.skybox = null;

        if (HasFog)
        {
            RenderSettings.fogMode = FogMode;
            RenderSettings.fogColor = FogColor;
            RenderSettings.fogStartDistance = StartDistance;
            RenderSettings.fogEndDistance = EndDistance;
        }

        LightmapSettings.lightmapsMode = LightmapsMode.NonDirectional;

        int l1 = (lightmapFar == null) ? 0 : lightmapFar.Length;
        int l2 = (lightmapNear == null) ? 0 : lightmapNear.Length;
        int l = (l1 < l2) ? l2 : l1;
        LightmapData[] lightmaps = null;
        if (l > 0)
        {
            lightmaps = new LightmapData[l];
            for (int i = 0; i < l; i++)
            {
                lightmaps[i] = new LightmapData();
                if (i < l1)
                    lightmaps[i].lightmapFar = lightmapFar[i];
                if (i < l2)
                    lightmaps[i].lightmapNear = lightmapNear[i];
            }
        }
        LightmapSettings.lightmaps = lightmaps;
    }
}