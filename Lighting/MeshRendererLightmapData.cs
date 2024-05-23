#pragma warning disable CS8618
using System;
using UnityEngine;

namespace TSUBASAMUSU.Lighting
{
    [Serializable]
    public class MeshRendererLightmapData
    {
        public string gameObjectName;

        public int lightmapIndex;

        public Vector4 lightmapScaleOffset;
    }
}