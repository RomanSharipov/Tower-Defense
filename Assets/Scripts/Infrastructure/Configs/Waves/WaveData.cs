using System;
using Assets.Scripts.CoreGamePlay;
using UnityEngine;

namespace CodeBase.Configs
{
    [Serializable]
    public class WaveData
    {
        [SerializeField] private int _countEnemy;
        [SerializeField] private float _delayBetweenSpawn;
        [SerializeField] private float _delayOnEndWave;
        [SerializeField] private EnemyConfig _enemyConfig;
        
        public int CountEnemy => _countEnemy;
        public float DelayBetweenSpawn => _delayBetweenSpawn;
        public float DelayOnEndWave => _delayOnEndWave;
        public EnemyConfig EnemyConfig => _enemyConfig;
    }
}
