using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class MinigunAttack : AttackComponent
    {
        [SerializeField] private ParticleSystemCollection[] _effects;

        private ParticleSystemCollection _currentEffects;

        public override void OnStartAttack(EnemyBase enemyBase)
        {
            base.OnStartAttack(enemyBase);
            _currentEffects.Play();
        }

        public override void OnEndAttack()
        {
            base.OnEndAttack();
            _currentEffects.Stop();
        }

        public override void Attack(EnemyBase enemyBase)
        {
            enemyBase.TakeDamage(_damage);
        }

        public override void SetLevel(int level)
        {
            _currentEffects = _effects[level];
        }
    }
}
