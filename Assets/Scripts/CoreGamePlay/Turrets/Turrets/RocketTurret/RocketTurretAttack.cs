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
        
        private BulletSpawnPoints _currentBulletSpawnPoint;

        public override void Attack(EnemyBase enemyBase)
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
