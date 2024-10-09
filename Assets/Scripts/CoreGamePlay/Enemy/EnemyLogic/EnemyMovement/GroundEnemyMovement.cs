using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System;
using CodeBase.Helpers;
using UniRx;
using VContainer;
using CodeBase.Infrastructure.Services;
using NTC.Pool;

namespace Assets.Scripts.CoreGamePlay
{
    public class GroundEnemyMovement : MonoBehaviour , IEnemyMovement,IDespawnable
    {
        [SerializeField] private CollisionAvoidance _collisionAvoidance;

        [Inject] private IBuildingService _buildingService;
        [Inject] private ICacherOfPath _cacherOfPath;

        private Transform _transtorm;
        
        private float _startSpeed = 2.1f;
        private float _currentSpeed;
        private float _slowdownСoefficient = 1.0f;

        [SerializeField] private float _rotateSpeed = 360f;
        
        private bool _isMoving = false;
        private float _yOffset = 0.41f;
        private float _distanceOfClosestTargetTile;

        private PathBuilder _pathBuilder;
        private EnemySpawner _enemySpawner;
        private TileData _currentTarget;
        private int _currentTargetIndex = 0;
        private List<TileData> _path;

        public float DistanceOfClosestTargetTile => _distanceOfClosestTargetTile;
        public TileData CurrentTarget => _currentTarget;
        public event Action GoalIsReached;
        
        public void Init(float startSpeed, EnemySpawner enemySpawner)
        {
            _buildingService.TurretIsBuilded += OnTurretIsBuilded;
            _enemySpawner = enemySpawner;
            _transtorm = transform;
            _startSpeed = startSpeed;
            _currentSpeed = _startSpeed;
            _pathBuilder = new PathBuilder();
            SetPath(_cacherOfPath.Paths[_enemySpawner]);
            SetCurrentTarget(0);
        }
        
        public void BlockTriggerOnCollisionAvoidance()
        {
            _collisionAvoidance.BlockTrigger().Forget();
        }

        public void SetPath(List<TileData> pathPoints)
        {
            _path = pathPoints;
            _pathBuilder.SetPath(pathPoints);
        }

        public void SetCurrentTarget(int tileIndex)
        {
            _currentTargetIndex = tileIndex;
            _currentTarget = _path[_currentTargetIndex];
        }
        
        public void StartMovement()
        {
            _isMoving = true;
        }

        public void StopMovement()
        {
            _isMoving = false;
        }

        public void UpdatePath(TileData tileData)
        {
            StopMovement();

            if (_pathBuilder.TryUpdatePath(tileData, _currentTargetIndex, out bool buildedRightInFrontOfUs, out bool newPathOppositeDirection))
            {
                if (buildedRightInFrontOfUs || newPathOppositeDirection)
                {
                    _currentTargetIndex--;
                    _currentTarget = _path[_currentTargetIndex];
                    _pathBuilder.UpdatePath(_currentTarget);

                }
                SetPath(_pathBuilder.Path);
                SetCurrentTarget(0);
            }

            StartMovement();
        }

        public void UnPause()
        {
            DOTween.To(() => _currentSpeed, x => _currentSpeed = x, _startSpeed, 0.5f);
        }

        public void Pause()
        {
            DOTween.To(() => _currentSpeed, x => _currentSpeed = x, 0f, 0.5f);
        }

        private void Update()
        {
            if (_isMoving)
            {
                MoveAlongPath();
            }
        }

        private void MoveAlongPath()
        {
            if (_currentTargetIndex >= _path.Count)
            {
                StopMovement();
                return;
            }

            _currentTarget = _path[_currentTargetIndex];
            MoveToTarget(_currentTarget);

            _distanceOfClosestTargetTile = Vector3.Distance(_transtorm.position, GetTargetPosition(_currentTarget));

            if (_distanceOfClosestTargetTile > 0.1f)
            {
                return;
            }

            HandleTileReached();
        }

        private bool IsLastTile()
        {
            return _currentTargetIndex + 1 >= _path.Count;
        }

        private void HandleTileReached()
        {
            _pathBuilder.RemoveCompletedTile(_currentTarget);

            if (IsLastTile())
            {
                CompletePathAndStop();
            }
            else
            {
                MoveToNextTile();
            }
        }

        private void MoveToNextTile()
        {
            _currentTargetIndex++;
            _currentTarget = _path[_currentTargetIndex];

            if (_currentTargetIndex >= _path.Count - 1)
            {
                CompletePathAndStop();
            }
        }

        private void CompletePathAndStop()
        {
            StopMovement();
            GoalIsReached?.Invoke();
        }

        private void MoveToTarget(TileData targetTile)
        {
            Vector3 target = GetTargetPosition(targetTile);

            _distanceOfClosestTargetTile = Vector3.Distance(_transtorm.position, target);
            if (_distanceOfClosestTargetTile > 0.1f)
            {
                MoveTowardsTarget(target);
                RotateTowardsTarget(target);
            }
        }

        private Vector3 GetTargetPosition(TileData targetTile)
        {
            Vector3 target = HexCalculator.ToWorldPosition(targetTile.Coords.Q, targetTile.Coords.R, 1.7f);
            target += Vector3.up * _yOffset;
            return target;
        }

        private void MoveTowardsTarget(Vector3 target)
        {
            Vector3 direction = (target - _transtorm.position).normalized;
            _transtorm.position += direction * _currentSpeed * Time.deltaTime * _slowdownСoefficient;
        }

        private void RotateTowardsTarget(Vector3 target)
        {
            Vector3 direction = (target - _transtorm.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _transtorm.rotation = Quaternion.RotateTowards(_transtorm.rotation, lookRotation, _rotateSpeed * Time.deltaTime);
        }

        public void SlowDownMovement(int percent,float duration)
        {
            percent = Math.Clamp(percent, 0, 100);

            _slowdownСoefficient = percent / 100.0f;

            Observable.Timer(TimeSpan.FromSeconds(duration))
                .Subscribe(_ => _slowdownСoefficient = 1.0f)
                .AddTo(this);
        }
        
        private void OnTurretIsBuilded(TurretBase turret, TileData tileData)
        {
            UpdatePath(tileData);
        }
        public void OnDespawn()
        {
            _buildingService.TurretIsBuilded -= OnTurretIsBuilded;
        }
    }
}