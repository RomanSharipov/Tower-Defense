using System;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class EnemyHealth : IEnemyHealth
    {
        public int MaxHealth { get; private set; }

        private int _currentHealth;
        private bool _healthIsOver = false;
        
        public event Action HealthIsOver;
        public event Action<int> HealthChanged;

        public EnemyHealth(int maxHealth)
        {
            MaxHealth = maxHealth;
            _currentHealth = MaxHealth;
        }

        public void ReduceHealth(int value)
        {
            if (_currentHealth == 0)
                return;

            value = Mathf.Max(value, 0);

            if (_currentHealth <= value)
            {
                _currentHealth = 0;
                if (_healthIsOver)
                    return;

                HealthIsOver?.Invoke();
                _healthIsOver = true;
            }
            else
            {
                _currentHealth -= value;
                HealthChanged?.Invoke(_currentHealth);
            }
        }
    }
}
