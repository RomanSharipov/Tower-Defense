using NTC.Pool;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class CannonShell : MonoBehaviour
    {
        private int _damage;
        private float _speed;

        public void Init(int damage, float speed)
        {
            _damage = damage;
            _speed = speed;
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
