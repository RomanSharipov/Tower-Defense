using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class FlameTurretAttack : MonoBehaviour, IAttackComponent
    {
        [SerializeField] private ParticleSystemCollection[] _effects;

        private ParticleSystemCollection _currentEffects;
        private int _damage;
        private EnemyBase _currentEnemy;

        public void SetConfig(int damage)
        {
            _damage = damage;
        }

        public void SetLevel(int level)
        {
            _currentEffects = _effects[level];
        }

        public void AttackIfNeeded()
        {
            _currentEnemy.TakeDamage(_damage);
        }

        public void OnStartAttack(EnemyBase enemyBase)
        {
            _currentEnemy = enemyBase;
            _currentEffects.Play();
        }

        public void OnEndAttack()
        {
            _currentEffects.Stop();
            _currentEnemy = null;   
        }

        private async UniTaskVoid SetEnemyOnFire(EnemyBase enemyBase)
        {

        }
    }
}
