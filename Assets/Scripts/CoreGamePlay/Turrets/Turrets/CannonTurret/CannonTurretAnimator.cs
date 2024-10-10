using System;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class CannonTurretAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public event Action<int> OnAnimationEventFire;

        public void PlayAttack()
        {
            _animator.SetTrigger("Attacking");
        }

        public void AnimationEventFire(int index)
        {
            OnAnimationEventFire?.Invoke(index);
        }
    }
}
