#pragma warning disable CS8600
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

            List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

            meshRenderers = GameObject.FindObjectsByType<MeshRenderer>(FindObjectsSortMode.None).ToList();

            foreach (MeshRendererLightmapData.Data data in meshRendererLightmapData.datas)
            {
                List<MeshRenderer> targetNameMeshRenderers = meshRenderers.FindAll(meshRenderer => meshRenderer.gameObject.name == data.gameObjectName);

                if (targetNameMeshRenderers.Count > 1)
                {
                    Debug.LogError("There are some same name GameObjects attached MeshRenderer Component named \"" + data.gameObjectName + "\".");

                    return false;
                }

                if (targetNameMeshRenderers == null || targetNameMeshRenderers.Count == 0)
                {
                    Debug.LogError("Failed to find a GameObject that is attached MeshRenderer Component named \"" + data.gameObjectName + "\".");

                    return false;
                }

                targetNameMeshRenderers[0].lightmapIndex = data.lightmapIndex;

                targetNameMeshRenderers[0].lightmapScaleOffset = data.lightmapScaleOffset;
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

        public static bool SortLightmaps(ref List<Texture2D> lightmaps)
        {
            List<(int index, Texture2D lightmap)> lightmapDatas = new List<(int index, Texture2D lightmap)>();

            foreach (Texture2D lightmap in lightmaps)
            {
                int index;

                try
                {
                    index = int.Parse(Regex.Replace(lightmap.name, @"[^0-9]", string.Empty));
                }
                catch (Exception exception)
                {
                    Debug.LogError("There is a incorrect name lightmap.It is \"" + lightmap.name + "\".");

                    Debug.LogException(exception);

                    return false;
                }

                lightmapDatas.Add((index, lightmap));
            }

            lightmapDatas = lightmapDatas.OrderBy(downloadColorLightmapData => downloadColorLightmapData.index).ToList();

            lightmaps = lightmapDatas.Select(downloadColorLightmapData => downloadColorLightmapData.lightmap).ToList();

            return true;
        }
    }
}