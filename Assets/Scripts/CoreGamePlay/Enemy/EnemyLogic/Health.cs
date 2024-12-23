using System;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class Health : IHealth
    {
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }
        
        private bool _healthIsOver = false;
        
        public event Action HealthIsOver;
        public event Action<int> HealthChanged;

        public Health(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = MaxHealth;
        }

        public void ReduceHealth(int value)
        {
            if (CurrentHealth == 0)
                return;

            value = Mathf.Max(value, 0);

            if (CurrentHealth <= value)
            {
                CurrentHealth = 0;
                if (_healthIsOver)
                    return;

                HealthIsOver?.Invoke();
                _healthIsOver = true;
            }
            else
            {
                CurrentHealth -= value;
                HealthChanged?.Invoke(CurrentHealth);
            }
        }
    }
}
