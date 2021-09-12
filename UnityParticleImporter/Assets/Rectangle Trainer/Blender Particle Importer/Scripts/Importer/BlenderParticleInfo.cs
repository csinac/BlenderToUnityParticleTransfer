using System;
using UnityEngine;

namespace RectangleTrainer.BlenderParticleTransfer
{
    [Serializable]
    public struct BlenderParticleInfo
    {
        public float birthTime;
        public BlenderParticleSnapshot[] snapshot;
        public int lifeTime;
    }

    [Serializable]
    public struct BlenderParticleSnapshot
    {
        public float x;
        public float y;
        public float z;

        public Vector3 Position => new Vector3(x, y, z);
    }
}