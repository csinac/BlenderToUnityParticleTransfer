using UnityEngine;

namespace RectangleTrainer.BlenderParticleTransfer.Render
{
    public class SimpleFrameBasedParticle : AFrameBasedParticleRenderer
    {
        [SerializeField] private AnimationCurve sizeOverLife = AnimationCurve.EaseInOut(0, 1, 1, 0);
        
        protected override void Render() {
            transform.position = particleInfo.snapshot[age].Position;

            float scale = 1f * age / particleInfo.lifeTime;
            scale = sizeOverLife.Evaluate(scale);
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
