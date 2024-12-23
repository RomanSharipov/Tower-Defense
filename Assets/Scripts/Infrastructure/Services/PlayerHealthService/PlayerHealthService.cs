using System;
using Assets.Scripts.CoreGamePlay;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Services
{
    public class PlayerHealthService : IPlayerHealthService,IInitializable
    {
        private Health _health;
        private int _maxHealth;
        
        [Inject]
        public PlayerHealthService(int maxHealth)
        {
            _maxHealth = maxHealth;
            ResetHealth();
        }

        public void ResetHealth()
        {
            _health = new Health(_maxHealth);
        }

        public int MaxHealth => _health.MaxHealth;

        public event Action<int> HealthChanged;
        public event Action HealthIsOver;

        public void Initialize()
        {
            _health.HealthChanged += OnHealthChanged;
            _health.HealthIsOver += OnHealthIsOver;
        }
        
        private void OnHealthIsOver()
        {
            HealthIsOver?.Invoke();
        }

        private void OnHealthChanged(int health)
        {
            HealthChanged?.Invoke(health);
        }

        public void ReduceHealth(int value)
        {
            _health.ReduceHealth(value);
        }
    }
}
