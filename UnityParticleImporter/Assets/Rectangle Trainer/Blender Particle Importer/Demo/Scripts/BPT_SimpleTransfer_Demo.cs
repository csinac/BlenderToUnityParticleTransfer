/*
 * Blender Particle Transfer Demo
 * Rectangle Trainer, 2021
 *
 * https://github.com/rectdev/BlenderToUnityParticleTransfer
 */

using RectangleTrainer.BlenderParticleTransfer.Importer;
using RectangleTrainer.BlenderParticleTransfer.Render;
using UnityEngine;

namespace RectangleTrainer.BlenderParticleTransfer.Demo
{
    public class BPT_SimpleTransfer_Demo : MonoBehaviour
    {
        [SerializeField] private BPTImporter importer;
        [SerializeField] private AFrameBasedParticleRenderer particleRendererPF;
        [SerializeField] private Transform particleContainer;
        [SerializeField] private int repeatDelay = 0;
        private int frame = 0;

        private void Start() {
            Application.targetFrameRate = 60; //in case realtime fps matches the desired output
            importer.Load();
        }

        private void Update() {
            if (!importer.Ready)
                return;

            AdvanceTime();
            SpawnParticles();
        }

        private void AdvanceTime() {
            frame++;
            if (frame > importer.EndFrame + repeatDelay)
                frame = importer.StartFrame;
        }

        private void SpawnParticles() {
            BlenderParticleInfo[] particles = importer.GetParticlesBornAt(frame);

            if (particles == null)
                return;
            
            foreach (BlenderParticleInfo p in particles) {
                AFrameBasedParticleRenderer pr = Instantiate(particleRendererPF, particleContainer);
                pr.Set(p);
            }
        }
    }
}
