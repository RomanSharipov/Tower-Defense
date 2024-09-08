using System;
using Assets.Scripts.CoreGamePlay;
using UnityEngine;

namespace CodeBase.Configs
{
    [Serializable]
    public class WaveData
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private int _countEnemy;
        [SerializeField] private float _delayBetweenSpawn;
        [SerializeField] private EnemyConfig _enemyConfig;
        
        public EnemyType EnemyType => _enemyType;
        public int CountEnemy => _countEnemy;
        public float DelayBetweenSpawn => _delayBetweenSpawn;
        public EnemyConfig EnemyConfig => _enemyConfig;
    }
}
