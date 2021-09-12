using UnityEngine;

namespace RectangleTrainer.BlenderParticleTransfer.Renderer
{
    public class SimpleFrameBasedParticleRenderer : AFrameBasedParticleRenderer
    {
        protected override void Render() {
            transform.position = particleInfo.snapshot[age].Position;
        }
    }
}
