using UnityEngine;

namespace RectangleTrainer.BlenderParticleTransfer.Render
{
    public class RandomHueFrameBasedParticle : SimpleFrameBasedParticle
    {
        [SerializeField] private MeshRenderer meshRenderer;
        
        public override void Set(BlenderParticleInfo particleInfo) {
            base.Set(particleInfo);
            if (meshRenderer) {
                float hue = (Mathf.Sin(particleInfo.birthTime * 234.2309f) + 1) / 2;
                meshRenderer.material.color = Color.HSVToRGB(hue, 0.24f, 1f);
            }
        }
    }
}
