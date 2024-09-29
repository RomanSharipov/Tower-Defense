using System;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public abstract class AttackComponent : MonoBehaviour, IUpgradeable
    {
        [SerializeField] private ParticleSystemCollection[] _effects;

        protected ParticleSystemCollection _currentEffects;

        private float _intervalBetweenAttack;
        protected int _damage;
        protected float _bulletSpeed;
        private float _attackTimer;
        private EnemyBase _currentEnemy;

        public abstract void Attack(EnemyBase enemyBase);

        public void SetConfig(float intervalBetweenAttack, int damage, float bulletSpeed)
        {
            _intervalBetweenAttack = intervalBetweenAttack;
            _damage = damage;
            _bulletSpeed = bulletSpeed;
        }

        public virtual void OnStartAttack(EnemyBase enemyBase)
        {
            _currentEnemy = enemyBase;
        }

        public virtual void OnEndAttack()
        {
            _currentEnemy = null;
        }
        
        public virtual void SetLevel(int level)
        {
            _currentEffects = _effects[level];
        }

        public void AttackIfNeeded()
        {
            _attackTimer -= Time.deltaTime;

            if (_attackTimer <= 0f)
            {
                _attackTimer = _intervalBetweenAttack;
                Attack(_currentEnemy);
            }
        }
    }
}
