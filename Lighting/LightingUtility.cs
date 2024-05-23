#pragma warning disable CS8600
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TSUBASAMUSU.UnityEditor;
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

        public static async Task<bool> CreateJsonFileForLightingAsync()
        {
            List<MeshRendererLightmapData.Data> datas = new List<MeshRendererLightmapData.Data>();

            MeshRenderer[] meshRenderers = GameObject.FindObjectsByType<MeshRenderer>(FindObjectsSortMode.None);

            foreach (MeshRenderer meshRenderer in meshRenderers)
            {
                datas.Add(new MeshRendererLightmapData.Data()
                {
                    gameObjectName = meshRenderer.gameObject.name,
                    lightmapIndex = meshRenderer.lightmapIndex,
                    lightmapScaleOffset = meshRenderer.lightmapScaleOffset,
                });
            }

            MeshRendererLightmapData meshRendererLightmapData = new MeshRendererLightmapData()
            {
                datas = datas,
            };

            string jsonString = JsonUtility.ToJson(meshRendererLightmapData);

            return await AssetUtility.CreateJsonFileAtRootDirectoryAsync(jsonString, "MeshRendererLightmapData");
        }
    }
}