using System.Collections.Generic;
using UnityEngine;

namespace TSUBASAMUSU.Other
{
    public static class LightingUtility
    {
        public static void ApplyLightmapsToCurrentScene(List<Texture2D> lightmaps)
        {
            LightmapSettings.lightmaps = new LightmapData[lightmaps.Count];

            for (int i = 0; i < lightmaps.Count; i++)
            {
                LightmapSettings.lightmaps[i] = new LightmapData
                {
                    lightmapColor = lightmaps[i]
                };
            }
        }
    }
}