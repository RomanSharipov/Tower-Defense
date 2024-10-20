using System;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class EnemyHealth : IEnemyHealth
    {
        public int MaxHealth { get; private set; }

        private int _currentHealth;

        public event Action HealthIsOver;
        public event Action<int> HealthChanged;

        public EnemyHealth(int maxHealth)
        {
            MaxHealth = maxHealth;
            _currentHealth = MaxHealth;
        }

        public void ReduceHealth(int value)
        {
            value = Mathf.Max(value, 0);

            if (_currentHealth <= value)
            {
                _currentHealth = 0;
                HealthIsOver?.Invoke();
            }
            else
            {
                _currentHealth -= value;
                HealthChanged?.Invoke(_currentHealth);
            }
        }
    }
}
