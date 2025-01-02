using System;
using Assets.Scripts.CoreGamePlay;

namespace CodeBase.Infrastructure.Services
{
    public interface IPlayerHealthService
    {
        public event Action HealthChanged;
        public int MaxHealth { get; }
        public int CurrentHealth { get; }

        public event Action HealthIsOver;

        public void ReduceHealth(int value);
        public void Init(int maxHealth);
        public void ResetHealth();
        public void Unsubscribe();
    }
}