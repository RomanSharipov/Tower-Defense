using System;
using NTC.Pool;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class CannonShell : MonoBehaviour
    {
        private int _damage;
        private float _speed;
        private float _maxLifeTime = 10.0f;
        private DetectorEnemies _detectorEnemies = new DetectorEnemies();
        private Collider[] _collider = new Collider[20];
        private LayerMask _enemyLayer;
        private float _damageDistance = 5.0f;

        public void Init(int damage, float speed, LayerMask enemyLayer)
        {
            _damage = damage;
            _speed = speed;
            _enemyLayer = enemyLayer;
            NightPool.Despawn(gameObject, _maxLifeTime);
        }

        private void Update()
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyBase enemy))
            {
                enemy.TakeDamage(_damage);
                NightPool.Despawn(gameObject);
            }

            DamageClosestEnemies();
            Array.Clear(_collider, 0, _collider.Length);
        }

        private void DamageClosestEnemies()
        {
            _detectorEnemies.TryFindEnemy(transform.position, _enemyLayer, _damageDistance, _collider, out EnemyBase totalEnemy);

            foreach (Collider item in _collider)
            {
                if (item == null)
                    return;

                item.GetComponent<EnemyBase>().TakeDamage( Convert.ToInt32(_damage / 2));
            }
        }
    }
}
