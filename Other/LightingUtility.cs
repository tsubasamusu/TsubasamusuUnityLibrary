using UnityEngine;

namespace TsubasamusuUnityLibrary.Other
{
    public static class LightingUtility
    {
        public static void ApplyLightmapsToCurrentScene(Texture2D[] colorLightmaps, Texture2D[] dirLightmaps)
        {
            LightmapData[] lightmapDatas = new LightmapData[colorLightmaps.Length];

            for (int i = 0; i < colorLightmaps.Length; i++)
            {
                LightmapData lightmapData = new LightmapData();

                lightmapData.lightmapColor = colorLightmaps[i];

                lightmapData.lightmapDir = dirLightmaps[i];

                lightmapDatas[i] = lightmapData;
            }

            LightmapSettings.lightmaps = lightmapDatas;
        }
    }
}