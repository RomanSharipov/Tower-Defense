using NTC.Pool;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class RocketShell : MonoBehaviour
    {
        private int _damage;
        private float _speed = 5.0f;
        private float _maxLifeTime = 10.0f;

        public void Init(int damage)
        {
            _damage = damage;
            NightPool.Despawn(gameObject,_maxLifeTime);
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
        }
    }
}
