using System;
using NTC.Pool;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class CannonTurretAttack : AttackComponent
    {
        [SerializeField] private CannonTurretAnimator[] _animators;
        [SerializeField] private BulletSpawnPoints[] _bulletSpawnPoints;
        [SerializeField] private CannonShell _bulletPrefab;
        
        private CannonTurretAnimator _currentAnimator;
        private BulletSpawnPoints _currentBulletSpawnPoint;
        
        public override void Attack(EnemyBase enemyBase)
        {
            _currentAnimator.PlayAttack();
        }

        public override void SetLevel(int level)
        {
            base.SetLevel(level);
            _currentAnimator = _animators[level];
            _currentBulletSpawnPoint = _bulletSpawnPoints[level];
        }

        public void OnAnimationEventFire(int gunIndex)
        {
            _currentEffects.GetParticleSystemByIndex(gunIndex).Play();
            CannonShell bullet = NightPool.Spawn(_bulletPrefab, _currentBulletSpawnPoint.GetSpawnPointByIndex(gunIndex).position, _currentBulletSpawnPoint.GetSpawnPointByIndex(gunIndex).rotation);
            bullet.Init(_damage, _bulletSpeed);
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
