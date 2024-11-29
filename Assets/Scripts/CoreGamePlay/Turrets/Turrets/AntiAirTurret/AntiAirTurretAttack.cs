using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class AntiAirTurretAttack : AttackComponent
    {
        [SerializeField] private TurretAnimator[] _animators;
        [SerializeField] private ParticleSystemCollection[] _effects;

        private float _intervalBetweenAttack;
        private int _damage;
        
        private TurretAnimator _currentAnimator;
        private ParticleSystemCollection _currentEffects;
        
        private Timer _timer;

        public void Init(float intervalBetweenAttack, int damage)
        {
            _intervalBetweenAttack = intervalBetweenAttack;
            _damage = damage;
            _timer = new Timer(_intervalBetweenAttack);
        }
        
        public override void AttackOnUpdate()
        {
            _timer.Tick();
            if (_timer.IsActionTimeReached())
            {
                _currentAnimator.PlayAttack();
            }
        }

        public override void SetLevel(int level)
        {
            _currentEffects = _effects[level];
            _currentAnimator = _animators[level];
        }

        public void OnAnimationEventFire(int gunIndex)
        {
            _currentEffects.GetParticleSystemByIndex(gunIndex).Play();

            if (EnemyIsDead)
                return;

            _currentEnemy.TakeDamage(_damage);
        }

        private void OnEnable()
        {
            foreach (TurretAnimator animator in _animators)
            {
                animator.OnAnimationEventFire += OnAnimationEventFire;
            }
        }

        private void OnDisable()
        {
            foreach (TurretAnimator animator in _animators)
            {
                animator.OnAnimationEventFire -= OnAnimationEventFire;
            }
        }
    }
}
