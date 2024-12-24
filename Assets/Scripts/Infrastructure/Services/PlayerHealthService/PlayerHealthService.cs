using System;
using Assets.Scripts.CoreGamePlay;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Services
{
    public class PlayerHealthService : IPlayerHealthService,IInitializable
    {
        private Health _health;
        private int _maxHealth;
        private IGameStatusService _gameStatusService;
        
        [Inject]
        public PlayerHealthService(int maxHealth, IGameStatusService gameStatusService)
        {
            _maxHealth = maxHealth;
            _health = new Health(_maxHealth);
            _gameStatusService = gameStatusService;
        }

        public void ResetHealth()
        {
            _health.ResetHealth();
        }

        public int MaxHealth => _health.MaxHealth;

        public int CurrentHealth => _health.CurrentHealth;

        public event Action HealthChanged;
        public event Action HealthIsOver;

        public void Initialize()
        {
            _health.HealthChanged += OnHealthChanged;
            _health.HealthIsOver += OnHealthIsOver;
        }
        
        private void OnHealthIsOver()
        {
            HealthIsOver?.Invoke();
            _gameStatusService.SetStatus(GameStatus.Lose);
        }

        private void OnHealthChanged(int health)
        {
            
            HealthChanged?.Invoke();
        }

        public void ReduceHealth(int value)
        {
            _health.ReduceHealth(value);
        }
    }
}
