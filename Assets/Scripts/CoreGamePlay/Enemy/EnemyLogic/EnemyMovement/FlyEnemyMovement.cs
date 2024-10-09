using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using UniRx;
using VContainer;
using CodeBase.Infrastructure.Services;

namespace Assets.Scripts.CoreGamePlay
{
    public class FlyEnemyMovement : MonoBehaviour , IEnemyMovement
    {
        [Inject] private ICacherOfPath _cacherOfPath;

        private Transform _transtorm;
        
        private float _startSpeed = 2.1f;
        private float _currentSpeed;
        private float _slowdownСoefficient = 1.0f;
        
        private bool _isMoving = false;
        
        private EnemySpawner _enemySpawner;
        public event Action GoalIsReached;
        
        public void SlowDownMovement(int percent, float duration)
        {
            percent = Math.Clamp(percent, 0, 100);

            _slowdownСoefficient = percent / 100.0f;

            Observable.Timer(TimeSpan.FromSeconds(duration))
                .Subscribe(_ => _slowdownСoefficient = 1.0f)
                .AddTo(this);
        }
        
        public void Init(float startSpeed, EnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
        }

        public void StartMovement()
        {
            _isMoving = true;
        }

        public void StopMovement()
        {
            _isMoving = false;
        }
        
        private void Update()
        {
            if (_isMoving)
            {
                
            }
        }
        private void CompletePathAndStop()
        {
            StopMovement();
            GoalIsReached?.Invoke();
        }
    }
}