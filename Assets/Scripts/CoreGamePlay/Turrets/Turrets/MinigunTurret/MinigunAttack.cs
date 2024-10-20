using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class MinigunAttack : AttackComponent
    {
        [SerializeField] private ParticleSystemCollection[] _effects;

        private ParticleSystemCollection _currentEffects;

        private float _intervalBetweenAttack;
        private int _damage;
        private Timer _timer;

        public void Init(float intervalBetweenAttack, int damage)
        {
            _intervalBetweenAttack = intervalBetweenAttack;
            _damage = damage;
            _timer = new Timer(_intervalBetweenAttack);
        }

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

        public override void AttackOnUpdate()
        {
            if (_timer.IsActionTimeReached())
            {
                _currentEnemy.TakeDamage(_damage);
            }
        }

        public override void SetLevel(int level)
        {
            _currentEffects = _effects[level];
            _damage *= (level + 1);
        }
    }
}
