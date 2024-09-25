using System;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    [Serializable]
    public class ParticleSystemCollection 
    {
        [SerializeField] private ParticleSystem[] _particleSystems;

        public void Play()
        {
            foreach (ParticleSystem particleSystem in _particleSystems)
            {
                particleSystem.Play();
            }
        }

        public void Stop()
        {
            foreach (ParticleSystem particleSystem in _particleSystems)
            {
                particleSystem.Stop();
            }
        }
    }
}
