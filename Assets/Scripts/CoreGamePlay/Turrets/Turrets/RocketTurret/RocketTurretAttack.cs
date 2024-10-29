using System;
using Cysharp.Threading.Tasks;
using NTC.Pool;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class RocketTurretAttack : AttackComponent
    {
        [SerializeField] private RocketShell _bulletPrefab;
        [SerializeField] private BulletSpawnPoints[] _bulletSpawnPoints;

        private float _intervalBetweenAttack;
        private float _bulletSpeed;
        private int _damage;
        private Timer _timer;

        private BulletSpawnPoints _currentBulletSpawnPoint;

        public void Init(float intervalBetweenAttack, int damage, float bulletSpeed)
        {
            _intervalBetweenAttack = intervalBetweenAttack;
            _damage = damage;
            _bulletSpeed = bulletSpeed;
            _timer = new Timer(_intervalBetweenAttack);
        }

        public override void AttackOnUpdate()
        {
            _timer.Tick();
            if (_timer.IsActionTimeReached())
            {
                Attack();
            }
        }

        private void Attack()
        {
            int bulletCount = _currentBulletSpawnPoint.SpawnPoints.Length;
            int bulletIndex = 0;

            Observable.Interval(TimeSpan.FromSeconds(0.5f))
                .Take(bulletCount)
                .Subscribe(_ =>
                {
                    Transform spawnPoint = _currentBulletSpawnPoint.SpawnPoints[bulletIndex];
                    RocketShell bullet = NightPool.Spawn(_bulletPrefab, spawnPoint.position, spawnPoint.rotation);
                    bullet.Init(_damage, _bulletSpeed);

                    bulletIndex++;
                }).AddTo(this);
        }

        public override void SetLevel(int level)
        {
            _currentBulletSpawnPoint = _bulletSpawnPoints[level];
        }
    }
}
