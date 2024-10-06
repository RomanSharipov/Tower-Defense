using NTC.Pool;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class SlowShell : MonoBehaviour
    {
        private int _damage;
        private float _speed;
        private float _maxLifeTime = 10.0f;

        public void Init(int damage, float speed)
        {
            _damage = damage;
            _speed = speed;
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
                enemy.Movement.SlowDownMovement(20);
                NightPool.Despawn(gameObject);
            }
        }
    }
}
