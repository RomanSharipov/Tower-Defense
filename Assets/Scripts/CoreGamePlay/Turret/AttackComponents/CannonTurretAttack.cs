using System;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class CannonTurretAttack : AttackComponent
    {
        [SerializeField] private CannonTurretAnimator[] _animators;

        private CannonTurretAnimator _currentAnimator;
        
        public override void Attack(EnemyBase enemyBase)
        {
            _currentAnimator.PlayAttack();
        }

        public override void SetLevel(int level)
        {
            base.SetLevel(level);
            _currentAnimator = _animators[level];
        }

        public void OnAnimationEventFire(int gunIndex)
        {
            _currentEffects.GetParticleSystemByIndex(gunIndex).Play();
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
