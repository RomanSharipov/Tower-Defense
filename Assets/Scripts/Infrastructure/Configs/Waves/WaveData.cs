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
        
        public EnemyType EnemyType => _enemyType;
        public int CountEnemy => _countEnemy;
        public float DelayBetweenSpawn => _delayBetweenSpawn;
    }
}
