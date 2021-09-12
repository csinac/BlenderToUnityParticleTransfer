using UnityEngine;

namespace RectangleTrainer.BlenderParticleTransfer.Render
{
    public abstract class AFrameBasedParticleRenderer : MonoBehaviour
    {
        protected BlenderParticleInfo particleInfo;
        protected int age;

        public virtual void Set(BlenderParticleInfo particleInfo) {
            this.particleInfo = particleInfo;
            age = 0;
            Render();
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
