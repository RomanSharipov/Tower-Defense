using System;
using NTC.Pool;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class CannonTurretAttack : AttackComponent
    {
        [SerializeField] private TurretAnimator[] _animators;
        [SerializeField] private BulletSpawnPoints[] _bulletSpawnPoints;
        [SerializeField] private CannonShell _bulletPrefab;
        [SerializeField] private ParticleSystemCollection[] _effects;

        private float _intervalBetweenAttack;
        private float _bulletSpeed;
        private int _damage;

        private TurretAnimator _currentAnimator;
        private BulletSpawnPoints _currentBulletSpawnPoint;
        private ParticleSystemCollection _currentEffects;
        private Timer _timer;

        public void Init(float intervalBetweenAttack, int damage, float bulletSpeed)
        {
            _intervalBetweenAttack = intervalBetweenAttack;
            _damage = damage;
            _bulletSpeed = bulletSpeed;
            _timer = new Timer(_intervalBetweenAttack);
        }

        public override void AttackOnUpdate()
        {
            if (_timer.IsActionTimeReached())
            {
                _currentAnimator.PlayAttack();
            }
        }

        public override void SetLevel(int level)
        {
            _currentEffects = _effects[level];
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
