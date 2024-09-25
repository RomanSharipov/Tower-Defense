using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class MinigunAttack : MonoBehaviour, IAttackComponent
    {
        [SerializeField] private ParticleSystemCollection[] _minigunEffects;

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
            _currentEffects = _minigunEffects[level];
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
