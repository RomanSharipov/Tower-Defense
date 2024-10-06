using System;
using System.Collections.Generic;
using CodeBase.Configs;
using UnityEngine;
using NTC.Pool;
using VContainer;

namespace Assets.Scripts.CoreGamePlay
{

    public abstract class EnemyBase : MonoBehaviour ,IDespawnable
    {
        [SerializeField] private EnemyMovement _movement; 
        [SerializeField] private TileView _testTile; 
        [SerializeField] private EnemyConfig _enemyConfig; 
        [SerializeField] private Collider _collider; 
        [SerializeField] private EnemyHealth _health; 
        
        public EnemyMovement Movement => _movement;
        public Vector3 Position => _collider.bounds.center;
        public bool AlreadyConstructed { get; private set; }

        public event Action<EnemyBase> GoalIsReached;
        public event Action<EnemyBase> Died;

        [Inject]
        public void Construct()
        {
            AlreadyConstructed = true;
        }

        public void Init(List<TileData> pathPoints, EnemyConfig enemyConfig)
        {
            _health.Init(enemyConfig.Health);
            _enemyConfig = enemyConfig;
            
            _movement.Init(enemyConfig.MovementSpeed);
            _movement.SetPath(pathPoints);
            _movement.SetCurrentTarget(0);
            _movement.StartMovement();
            _movement.GoalIsReached += OnGoalIsReached;
            _health.HealthIsOver += OnHealthIsOver;

        }

        private void OnHealthIsOver()
        {
            NightPool.Despawn(gameObject);
        }

        private void OnGoalIsReached()
        {
            GoalIsReached?.Invoke(this);
        }
        
        [ContextMenu("TakeDamage()")]
        public void TakeDamage(int damage)
        {
            _health.ReduceHealth(damage);
        }
        
        public void OnDespawn()
        {
            Died?.Invoke(this);
            _movement.StopMovement();
            _movement.GoalIsReached -= OnGoalIsReached;
            _health.HealthIsOver -= OnHealthIsOver;
        }
    }
}
