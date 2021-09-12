using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace RectangleTrainer.BlenderParticleTransfer.Importer
{
    public class BPTImporter : MonoBehaviour
    {
        [SerializeField] private Object bptFile;

        private int particleCount;
        private int startFrame;
        private int endFrame;

        public int ParticleCount => particleCount;
        public int StartFrame => startFrame;
        public int EndFrame => endFrame;

        private bool ready = false;
        public bool Ready => ready;
        
        
        private Dictionary<int, List<BlenderParticleInfo>> particleBirthDict = new Dictionary<int, List<BlenderParticleInfo>>();

        private void Start() {
            Load();
        }

        public void Load() {
            StartCoroutine(LoadCR());
        }
        
        private IEnumerator LoadCR() {
            int head = 0;

            byte[] bytes = File.ReadAllBytes(AssetDatabase.GetAssetPath(bptFile));
            float[] packInfo = new float[4];
            Buffer.BlockCopy(bytes, head, packInfo, 0, sizeof(float) * 3);
            head += sizeof(float) * 3;
            
            particleCount = (int)packInfo[0];
            startFrame = (int) packInfo[1];
            endFrame = (int) packInfo[2];

            for (int i = 0; i < particleCount; i++) {
                if(i % 1000 == 0)
                    Debug.Log($"{i}/{particleCount}");
            
                float[] particleInfo = new float[2];
                Buffer.BlockCopy(bytes, head, particleInfo, 0, sizeof(float) * 2);
                head += sizeof(float) * 2;
                
                int len = Mathf.FloorToInt(particleInfo[0]);

                BlenderParticleInfo particle = new BlenderParticleInfo {
                    birthTime = particleInfo[1],
                    snapshot = new BlenderParticleSnapshot[len],
                    lifeTime = len
                };

                int birthRounded = Mathf.RoundToInt(particle.birthTime);
            
                float[] particleData1D = new float[len * 3];
                Buffer.BlockCopy(bytes, head, particleData1D, 0, sizeof(float) * particleData1D.Length);
                head += sizeof(float) * particleData1D.Length;
                
                for (int j = 0; j < len; j ++) {
                    particle.snapshot[j].x = particleData1D[j * 3];
                    particle.snapshot[j].y = particleData1D[j * 3 + 1];
                    particle.snapshot[j].z = particleData1D[j * 3 + 2];
                }

                if (!particleBirthDict.ContainsKey(birthRounded)) {
                    particleBirthDict.Add(birthRounded, new List<BlenderParticleInfo>());
                }
            
                particleBirthDict[birthRounded].Add(particle);
            }

            ready = true;
            yield return null;
        }
        
        public BlenderParticleInfo[] GetParticlesBornAt(int frame) {
            if (particleBirthDict.ContainsKey(frame)) {
                return particleBirthDict[frame].ToArray();
            }

            return null;
        }
    }
}