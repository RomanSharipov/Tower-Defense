using NTC.Pool;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class RocketTurretAttack : AttackComponent
    {
        [SerializeField] private SlowShell _bulletPrefab;
        [SerializeField] private Transform[] _bulletSpawnPoints;
        [SerializeField] private ParticleSystemCollection[] _effects;

        private ParticleSystemCollection _currentEffects;
        private Transform _currentBulletSpawnPoint;

        public override void OnStartAttack(EnemyBase enemyBase)
        {
            base.OnStartAttack(enemyBase);
            _currentEffects.Play();
        }

        public override void OnEndAttack()
        {
            base.OnEndAttack();
            _currentEffects.Stop();
        }

        public override void Attack(EnemyBase enemyBase)
        {
            SlowShell bullet = NightPool.Spawn(_bulletPrefab, _currentBulletSpawnPoint.position, _currentBulletSpawnPoint.rotation);
            bullet.Init(_damage, _bulletSpeed);
        }

        public override void SetLevel(int level)
        {
            _currentEffects = _effects[level];
            _currentBulletSpawnPoint = _bulletSpawnPoints[level];
        }
    }
}
