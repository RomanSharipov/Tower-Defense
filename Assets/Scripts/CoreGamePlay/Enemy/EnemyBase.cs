using System;
using CodeBase.Configs;
using UnityEngine;
using NTC.Pool;
using VContainer;
using CodeBase.Infrastructure.UI;

namespace Assets.Scripts.CoreGamePlay
{

    public abstract class EnemyBase : MonoBehaviour ,IDespawnable
    {
        [SerializeField] private IEnemyMovement _movement; 
        
        [SerializeField] private EnemyConfig _enemyConfig; 
        [SerializeField] private Collider _collider; 
        [SerializeField] private HealthBar _healthBar;

        private IEnemyHealth _health;
        
        public abstract IEnemyMovement EnemyMovement { get; }
        
        public Vector3 Position => _collider.bounds.center;

        public HealthBar HealthBar => _healthBar;

        public bool AlreadyConstructed { get; private set; }

        public event Action<EnemyBase> GoalIsReached;
        public event Action<EnemyBase> Died;

        [Inject]
        public void Construct()
        {
            AlreadyConstructed = true;
        }

        public void Init(EnemySpawner enemySpawner, EnemyConfig enemyConfig, IEnemyHealth enemyHealth)
        {
            _health = enemyHealth;
            _enemyConfig = enemyConfig;
            EnemyMovement.Init(enemyConfig.MovementSpeed, enemySpawner);

            EnemyMovement.StartMovement();
            EnemyMovement.GoalIsReached += OnGoalIsReached;
            _health.HealthIsOver += OnHealthIsOver;
        }

        private void OnHealthIsOver()
        {
            Died?.Invoke(this);
        }

        private void OnGoalIsReached()
        {
            GoalIsReached?.Invoke(this);
        }
        
        public void TakeDamage(int damage)
        {
            _health.ReduceHealth(damage);
        }
        
        public void OnDespawn()
        {
            EnemyMovement.StopMovement();
            EnemyMovement.GoalIsReached -= OnGoalIsReached;
            _health.HealthIsOver -= OnHealthIsOver;
            _healthBar.OnDespawn();
        }
    }
}
