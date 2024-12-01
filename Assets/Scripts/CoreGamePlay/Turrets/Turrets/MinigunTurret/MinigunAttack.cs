using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class MinigunAttack : AttackComponent
    {
        [SerializeField] private ParticleSystemCollection[] _effects;
        [SerializeField] private MinigunAnimation[] _minigunAnimation;

        private ParticleSystemCollection _currentEffects;
        private MinigunAnimation _currentMinigunAnimation;

        private float _intervalBetweenAttack = 0.1f;
        [SerializeField] private int _currentDamage;
        private int _startDamage;
        private Timer _timer;

        private float[] _levelCoefficient = new float[]
        {
            1.0f,
            1.5f,
            2.0f
        };

        public void Init(int damage)
        {
            _startDamage = damage;
            _currentDamage = _startDamage;
            _timer = new Timer(_intervalBetweenAttack);
        }

        public override void OnStartAttack(EnemyBase enemyBase)
        {
            base.OnStartAttack(enemyBase);
            _currentEffects.Play();
            _currentMinigunAnimation.StartAnimation();

        }

        public override void OnEndAttack()
        {
            base.OnEndAttack();
            _currentEffects.Stop();
            _currentMinigunAnimation.StopAnimation();
        }

        public override void AttackOnUpdate()
        {
            _timer.Tick();
            if (_timer.IsActionTimeReached())
            {
                _currentEnemy.TakeDamage(_currentDamage);
            }
        }

        public override void SetLevel(int level)
        {
            _currentEffects = _effects[level];
            _currentMinigunAnimation = _minigunAnimation[level];
            _currentDamage = Mathf.RoundToInt(_levelCoefficient[level] * _startDamage);
        }
    }
}
