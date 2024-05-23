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

        public static void ApplyLightmapToMeshRenderer(MeshRendererLightmapData meshRendererLightmapData)
        {
            GameObject meshRendererGameObject = GameObject.Find(meshRendererLightmapData.gameObjectName);

            if (meshRendererGameObject == null)
            {
                Debug.LogError("Failed to get GameObject named \"" + meshRendererLightmapData.gameObjectName + "\".");

                return;
            }

            if (!meshRendererGameObject.TryGetComponent(out MeshRenderer meshRenderer))
            {
                Debug.LogError("Failed to get MeshRenderer from \"" + meshRendererLightmapData.gameObjectName + "\".");

                return;
            }

            meshRenderer.lightmapIndex = meshRendererLightmapData.lightmapIndex;

            meshRenderer.lightmapScaleOffset = meshRendererLightmapData.lightmapScaleOffset;
        }
    }
}