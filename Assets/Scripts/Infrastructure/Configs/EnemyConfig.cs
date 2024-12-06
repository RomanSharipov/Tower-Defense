using System;
using Assets.Scripts.CoreGamePlay;
using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "StaticData/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private int _health;
        [SerializeField] private Material[] _materials;
        [SerializeField] private EnemyType _enemyType;

        public float MovementSpeed => _movementSpeed;
        public int Health => _health;
        public Material[] Materials => _materials;
        public EnemyType EnemyType => _enemyType;
    }
}
