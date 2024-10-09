using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using UniRx;
using VContainer;
using CodeBase.Infrastructure.Services;
using BezierSolution;
using NTC.Pool;

namespace Assets.Scripts.CoreGamePlay
{
    public class FlyEnemyMovement : MonoBehaviour , IEnemyMovement,IDespawnable
    {
        [Inject] private ICacherOfPath _cacherOfPath;
        [SerializeField] private BezierWalkerWithSpeed _bezierWalkerWithSpeed;

        [SerializeField] private float _startSpeed;
        
        [SerializeField] private float _slowdownСoefficient = 1.0f;
        
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
            _bezierWalkerWithSpeed.onPathCompleted.AddListener(OnPathCompleted);
            _bezierWalkerWithSpeed.spline = _cacherOfPath.PathFly[_enemySpawner];
            _startSpeed = startSpeed;
            _slowdownСoefficient = 1.0f;
        }

        private void Update()
        {
            _bezierWalkerWithSpeed.speed = _startSpeed * _slowdownСoefficient;
        }

        private void OnPathCompleted()
        {
            StopMovement();
            GoalIsReached?.Invoke();
            _bezierWalkerWithSpeed.NormalizedT = 0.0f;
        }

        public void StartMovement()
        {
            _bezierWalkerWithSpeed.enabled = true;
        }

        public void StopMovement()
        {
            _bezierWalkerWithSpeed.enabled = false;
        }
        
        public void OnDespawn()
        {
            _bezierWalkerWithSpeed.onPathCompleted.RemoveListener(OnPathCompleted);
        }
    }
}