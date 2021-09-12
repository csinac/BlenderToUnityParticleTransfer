using UnityEngine;

namespace RectangleTrainer.BlenderParticleTransfer
{
    public struct BlenderParticleInfo
    {
        public float birthTime;
        public BlenderParticleSnapshot[] snapshot;
        public int lifeTime;
    }

    public struct BlenderParticleSnapshot
    {
        public float x;
        public float y;
        public float z;

        public Vector3 Position => new Vector3(x, y, z);
    }
}