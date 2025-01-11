using System;
using Assets.Scripts.CoreGamePlay;
using UnityEngine;

namespace CodeBase.Configs
{
    [Serializable]
    public class WaveData
    {
        [SerializeField] private SubWaveData[] _subWaveData;

        public SubWaveData[] SubWaveData => _subWaveData;
    }
}
