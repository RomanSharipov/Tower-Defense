using System;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class CannonTurretAttack : MonoBehaviour, IAttackComponent
    {
        [SerializeField] private ParticleSystemCollection[] _effects;

        private ParticleSystemCollection _currentEffects;

        private float _intervalBetweenAttack;
        private int _damage;
        private float _attackTimer;
        private EnemyBase _currentEnemy;

        public void SetConfig(float intervalBetweenAttack, int damage)
        {
            _intervalBetweenAttack = intervalBetweenAttack;
            _damage = damage;
        }

        public void OnStartAttack(EnemyBase enemyBase)
        {
            _currentEnemy = enemyBase;
            _currentEffects.Play();
        }

        public void OnEndAttack()
        {
            _currentEffects.Stop();
        }
        
        public void SetLevel(int level)
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
        private void Attack(EnemyBase enemyBase)
        {
            enemyBase.TakeDamage(_damage);
        }
    }
}
