using System;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class CannonTurretAttack : AttackComponent
    {
        [SerializeField] private Animator[] _animators;

        private Animator _currentAnimator;

        public override void Attack(EnemyBase enemyBase)
        {
            _currentAnimator.SetTrigger("Attacking");
            
        }
        public override void SetLevel(int level)
        {
            base.SetLevel(level);
            _currentAnimator = _animators[level];
        }

        public void AnimationEventFire()
        {
            _currentEffects.Play();
        }
    }
}
