using System;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class EnemyHealth : MonoBehaviour ,IEnemyHealth
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private ReactiveProperty<int> _currentHealth = new ReactiveProperty<int>();
        
        public int MaxHealth => _maxHealth;

        public IReactiveProperty<int> CurrentHealth => _currentHealth;
        
        public event Action HealthIsOver;
        
        public void Init(int maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth.Value = _maxHealth;
        }

        public void ReduceHealth(int value)
        {
            value = Mathf.Max(value, 0);

            if (_currentHealth.Value <= value)
            {
                _currentHealth.Value = 0;
                HealthIsOver?.Invoke();
            }
            else
            {
                _currentHealth.Value -= value;
            }
        
        }
    }
}
