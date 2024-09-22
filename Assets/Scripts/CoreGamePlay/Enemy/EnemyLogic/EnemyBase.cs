using System;
using System.Collections.Generic;
using CodeBase.Configs;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{

    public abstract class EnemyBase : MonoBehaviour 
    {
        [SerializeField] private EnemyMovement _movement; 
        [SerializeField] private TileView _testTile; 
        [SerializeField] private EnemyConfig _enemyConfig; 
        [SerializeField] private Collider _collider; 
        [SerializeField] private EnemyHealth _health; 
        
        public EnemyMovement Movement => _movement;
        public Vector3 Position => _collider.bounds.center;

        public event Action<EnemyBase> GoalIsReached;

        public void Init(List<TileData> pathPoints, EnemyConfig enemyConfig)
        {
            _health.Init(200);
            _enemyConfig = enemyConfig;
            
            _movement.Init(enemyConfig.MovementSpeed);
            _movement.SetPath(pathPoints);
            _movement.SetCurrentTarget(0);
            _movement.StartMovement();
            _movement.GoalIsReached += OnGoalIsReached;
        }

        private void OnGoalIsReached()
        {
            GoalIsReached?.Invoke(this);
        }

        private void OnDestroy()
        {
            _movement.StopMovement();
            _movement.GoalIsReached -= OnGoalIsReached;
        }

        [ContextMenu("TakeDamage()")]
        private void TakeDamage()
        {
            _health.ReduceHealth(20);
        }


    }
}
