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
        [SerializeField] private IEnemyMovement _movement; 
        
        [SerializeField] private EnemyConfig _enemyConfig; 
        [SerializeField] private Collider _collider; 
        [SerializeField] private EnemyHealth _health;
        
        public abstract IEnemyMovement EnemyMovement { get; }
        
        public Vector3 Position => _collider.bounds.center;
        public bool AlreadyConstructed { get; private set; }

        public event Action<EnemyBase> GoalIsReached;
        public event Action<EnemyBase> Died;

        [Inject]
        public void Construct()
        {
            AlreadyConstructed = true;
        }

        public void Init(EnemySpawner enemySpawner, EnemyConfig enemyConfig)
        {
            _enemyConfig = enemyConfig;
            _health.Init(enemyConfig.Health);

            EnemyMovement.Init(enemyConfig.MovementSpeed, enemySpawner);

            EnemyMovement.StartMovement();
            EnemyMovement.GoalIsReached += OnGoalIsReached;
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
            EnemyMovement.StopMovement();
            EnemyMovement.GoalIsReached -= OnGoalIsReached;
            _health.HealthIsOver -= OnHealthIsOver;
        }
    }
}
