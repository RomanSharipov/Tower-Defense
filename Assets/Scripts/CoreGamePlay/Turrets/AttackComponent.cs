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
        
        public virtual void OnStartAttack(EnemyBase enemyBase)
        {
            _currentEnemy = enemyBase;
        }

        public virtual void OnEndAttack()
        {
            _currentEnemy = null;
        }

        public abstract void SetLevel(int level);
    
    }
}
