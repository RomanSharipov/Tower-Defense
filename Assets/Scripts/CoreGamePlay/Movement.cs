using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using Assets.Scripts.Helpers;
using System.Collections.Generic;
using Tarodev_Pathfinding._Scripts;
using System;

public class Movement
{
    private TileData[] _path;
    private readonly Transform _myTranstorm;
    private float _moveSpeed = 5f;
    private float _rotateSpeed = 360f;

    private int _currentTargetIndex = 0;
    private bool _isMoving = false;
    private CancellationTokenSource _cancellationTokenSource;
    private float _yOffset = 0.41f;
    private HashSet<TileData> _remaingsPath = new HashSet<TileData>();

    public Movement(Transform myTranstorm)
    {
        _myTranstorm = myTranstorm;
    }

    public void SetPath(TileData[] pathPoints)
    {
        _path = pathPoints;
        _remaingsPath.Clear();
        foreach (TileData pathPoint in _path)
        {
            _remaingsPath.Add(pathPoint);
        }
    }

    public void StartMovement()
    {
        StopMovement();
        _isMoving = true;
        _cancellationTokenSource = new CancellationTokenSource();
        MoveAlongPath().Forget();
    }

    public void StopMovement()
    {
        if (_cancellationTokenSource != null)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }

        _isMoving = false;
    }

    public void UpdatePathIfNeeded(TileData newUnwalkableTile)
    {
        StopMovement();
        if (_remaingsPath.Contains(newUnwalkableTile))
        {
            TileData[] newPath = Pathfinding.FindPath(_path[_currentTargetIndex], _path[_path.Length - 1]).ToArray();
            Array.Reverse(newPath);
            SetPath (newPath);
        }
        StartMovement();
    }

    private async UniTaskVoid MoveAlongPath() 
    {
        while (_isMoving && _currentTargetIndex < _path.Length)
        {
            Vector3 targetPoint = HexCalculator.ToWorldPosition(_path[_currentTargetIndex].Coords.Q, _path[_currentTargetIndex].Coords.R,1.7f);
            await MoveToTarget(targetPoint + Vector3.up * _yOffset);
            _remaingsPath.Remove(_path[_currentTargetIndex]);
            _currentTargetIndex++;
        }
    }

    private async UniTask MoveToTarget(Vector3 target)
    {
        while (_isMoving && Vector3.Distance(_myTranstorm.position, target) > 0.1f)
        {
            MoveTowardsTarget(target);
            RotateTowardsTarget(target);
            await UniTask.Yield(_cancellationTokenSource.Token);
        }
    }

    private void MoveTowardsTarget(Vector3 target)
    {
        Vector3 direction = (target - _myTranstorm.position).normalized;
        _myTranstorm.position += direction * _moveSpeed * Time.deltaTime;
    }

    private void RotateTowardsTarget(Vector3 target)
    {
        Vector3 direction = (target - _myTranstorm.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        _myTranstorm.rotation = Quaternion.RotateTowards(_myTranstorm.rotation, lookRotation, _rotateSpeed * Time.deltaTime);
    }
}
