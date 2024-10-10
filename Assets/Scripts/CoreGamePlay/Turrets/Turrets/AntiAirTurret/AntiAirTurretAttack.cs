using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class AntiAirTurretAttack : AttackComponent
    {
        [SerializeField] private CannonTurretAnimator[] _animators;
        [SerializeField] private ParticleSystemCollection[] _effects;
        
        private CannonTurretAnimator _currentAnimator;
        private ParticleSystemCollection _currentEffects;
        private EnemyBase _enemyBase;

        public override void Attack(EnemyBase enemyBase)
        {
            _currentAnimator.PlayAttack();
            _enemyBase = enemyBase;
        }

        public override void SetLevel(int level)
        {
            _currentEffects = _effects[level];
            _currentAnimator = _animators[level];
        }

        public void OnAnimationEventFire(int gunIndex)
        {
            _currentEffects.GetParticleSystemByIndex(gunIndex).Play();
            _enemyBase.TakeDamage(_damage);
        }

        private void OnEnable()
        {
            foreach (CannonTurretAnimator animator in _animators)
            {
                animator.OnAnimationEventFire += OnAnimationEventFire;
            }
        }

        private void OnDisable()
        {
            foreach (CannonTurretAnimator animator in _animators)
            {
                animator.OnAnimationEventFire -= OnAnimationEventFire;
            }
        }
    }
}
