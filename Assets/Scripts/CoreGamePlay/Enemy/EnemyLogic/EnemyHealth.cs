using System;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class EnemyHealth
    {
        private readonly int _maxHealth;
        private int _currentHealth;

        public event Action<int,int> HealthChanged;

        public EnemyHealth(int maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = _maxHealth;
        }

        public void ReduceHealth(int value)
        {
            value = Mathf.Max(value, 0);
            _currentHealth -= value;
            _currentHealth = Mathf.Max(_currentHealth, 0);
            HealthChanged?.Invoke(_currentHealth, _maxHealth);
        }
    }
}
