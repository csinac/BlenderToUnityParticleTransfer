using System.Collections;
using System.Collections.Generic;
using RectangleTrainer.BlenderParticleTransfer.Importer;
using RectangleTrainer.BlenderParticleTransfer.Renderer;
using UnityEngine;

namespace RectangleTrainer.BlenderParticleTransfer.Demo
{
    public class BPT_SimpleTransfer_Demo : MonoBehaviour
    {
        [SerializeField] private BPTImporter importer;
        [SerializeField] private AFrameBasedParticleRenderer particleRendererPF;
        [SerializeField] private Transform particleContainer;
        private int frame = 0;

        private void Start() {
            Application.targetFrameRate = 60;
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
            if (frame > importer.EndFrame)
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
