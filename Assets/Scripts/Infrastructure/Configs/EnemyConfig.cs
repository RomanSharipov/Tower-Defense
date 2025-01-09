using System;
using Assets.Scripts.CoreGamePlay;
using UnityEngine;

namespace CodeBase.Configs
{
    [Serializable]
    public class EnemyConfig 
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private int _health;
        [SerializeField] private Material[] _materials;
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private int _reward;

        public float MovementSpeed => _movementSpeed;
        public int Health => _health;
        public Material[] Materials => _materials;
        public EnemyType EnemyType => _enemyType;
        public int Reward => _reward;
    }
}
