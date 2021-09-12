using UnityEngine;

namespace RectangleTrainer.BlenderParticleTransfer.Renderer
{
    public abstract class AFrameBasedParticleRenderer : MonoBehaviour
    {
        protected BlenderParticleInfo particleInfo;
        protected int age;

        public void Set(BlenderParticleInfo particleInfo) {
            this.particleInfo = particleInfo;
            age = 0;
        }

        protected abstract void Render();

        private void Update() {
            if (age >= particleInfo.lifeTime) {
                Destroy(gameObject);
                return;
            }

            Render();
            age++;
        }
    }
}
