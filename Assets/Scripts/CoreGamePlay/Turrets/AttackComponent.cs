using System;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.CoreGamePlay
{
    public abstract class AttackComponent : MonoBehaviour, IUpgradeable
    {
        protected EnemyBase _currentEnemy;
        [Inject] protected ITurretsStatsProvider _turretsStatsProvider;
        public abstract void AttackOnUpdate();
        public bool EnemyIsDead { get; private set; }
        
        public virtual void OnStartAttack(EnemyBase enemyBase)
        {
            _currentEnemy = enemyBase;
            _currentEnemy.Died += OnEnemyDied;
            EnemyIsDead = false;
        }

        private void OnEnemyDied(EnemyBase enemy)
        {
            EnemyIsDead = false;
            _currentEnemy.Died -= OnEnemyDied;
        }

        public virtual void OnEndAttack()
        {
            EnemyIsDead = true;
            _currentEnemy.Died -= OnEnemyDied;
            _currentEnemy = null;
        }

        public abstract void SetLevel(int level);
    
    }
}
