using System;
using System.Collections.Generic;
using CodeBase.Configs;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public abstract class EnemyBase : MonoBehaviour 
    {
        [SerializeField] public EnemyMovement _movement; 
        [SerializeField] public TileView _testTile; 
        [SerializeField] public EnemyConfig _enemyConfig; 
        
        public EnemyMovement Movement => _movement;

        public event Action<EnemyBase> GoalIsReached;

        public void Init(List<TileData> pathPoints, EnemyConfig enemyConfig)
        {
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
    }
}
