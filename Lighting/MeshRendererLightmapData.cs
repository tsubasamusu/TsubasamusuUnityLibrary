#pragma warning disable CS8618
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TSUBASAMUSU.Lighting
{
    [Serializable]
    public class MeshRendererLightmapData
    {
        public List<Data> datas;

        [Serializable]
        public class Data
        {
            public string gameObjectName;

            public int lightmapIndex;

            public Vector4 lightmapScaleOffset;
        }
    }
}