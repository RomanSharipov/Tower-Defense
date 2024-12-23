using System;
using UniRx;

namespace Assets.Scripts.CoreGamePlay
{
    public interface IHealth
    {
        public event Action<int> HealthChanged;
        public int MaxHealth { get; }
        public int CurrentHealth { get; }

        public event Action HealthIsOver;

        public void ReduceHealth(int value);
    }
}