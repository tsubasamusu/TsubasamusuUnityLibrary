#pragma warning disable CS8600
using Newtonsoft.Json;
using System;
using UnityEngine;

namespace TSUBASAMUSU.Lighting
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

        public static bool ApplyLightmapsToMeshRenderers(TextAsset jsonFile)
        {
            MeshRendererLightmapData meshRendererLightmapData;

            try
            {
                meshRendererLightmapData = JsonConvert.DeserializeObject<MeshRendererLightmapData>(jsonFile.text);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);

                return false;
            }

            if (meshRendererLightmapData == null)
            {
                Debug.LogError("Failed to convert from the JSON file named \"" + jsonFile.name + "\".");

                return false;
            }

            foreach (MeshRendererLightmapData.Data data in meshRendererLightmapData.datas)
            {
                GameObject meshRendererGameObject = GameObject.Find(data.gameObjectName);

                if (meshRendererGameObject == null)
                {
                    Debug.LogError("Failed to get a GameObject named \"" + data.gameObjectName + "\".");

                    return false;
                }

                if (!meshRendererGameObject.TryGetComponent(out MeshRenderer meshRenderer))
                {
                    Debug.LogError("Failed to get a MeshRenderer from \"" + data.gameObjectName + "\".");

                    return false;
                }

                meshRenderer.lightmapIndex = data.lightmapIndex;

                meshRenderer.lightmapScaleOffset = data.lightmapScaleOffset;
            }

            return true;
        }
    }
}