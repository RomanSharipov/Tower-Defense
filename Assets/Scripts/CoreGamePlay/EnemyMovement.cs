using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using Assets.Scripts.Helpers;
using System.Collections.Generic;
using Tarodev_Pathfinding._Scripts;
using System;

[Serializable]
public class EnemyMovement : MonoBehaviour
{
    private TileData[] _path;
    private TileData _currentTarget;
    
    private Transform _myTranstorm;
    
    [SerializeField] private float _moveSpeed = 2.1f;
    private float _rotateSpeed = 360f;
    
    private int _currentTargetIndex = 0;
    private bool _isMoving = false;
    private CancellationTokenSource _cancellationTokenSource;
    private float _yOffset = 0.41f;
    private float _distanceOfClosestTargetTile;
    private HashSet<TileData> _remaingsPath = new HashSet<TileData>();

    public float DistanceOfClosestTargetTile => _distanceOfClosestTargetTile;

    public void NewMovement(Transform myTranstorm, string name)
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

    public async UniTask UpdatePathIfNeeded(TileData newUnwalkableTile)
    {
        if (PathContainsTile(newUnwalkableTile))
        {
            await UpdatePath(newUnwalkableTile);
        }
    }

    public bool PathContainsTile(TileData newUnwalkableTile)
    {
        return _remaingsPath.Contains(newUnwalkableTile);
    }

    public async UniTask UpdatePath(TileData newUnwalkableTile)
    {
        StopMovement();
        if (newUnwalkableTile == _currentTarget)
        {
            _currentTargetIndex--;
            _currentTarget = _path[_currentTargetIndex];
        }

        await MoveToTarget(_currentTarget);

        StopMovement();

        TileData[] newPath;
        List<TileData> newListPath = Pathfinding.FindPath(_currentTarget, _path[_path.Length - 1]);
        newListPath.Add(_currentTarget);
        newListPath.Reverse();
        newPath = newListPath.ToArray();


        SetPath(newPath);
        _currentTargetIndex = 0;


        StartMovement();
        return;
    }

    private async UniTaskVoid MoveAlongPath() 
    {
        while (_isMoving && _currentTargetIndex < _path.Length)
        {
            _currentTarget = _path[_currentTargetIndex];
            
            
            await MoveToTarget(_currentTarget);
            _remaingsPath.Remove(_currentTarget);
            
            if (_currentTargetIndex + 1 >= _path.Length)
            {
                StopMovement();
                return;
            }
            
            _currentTargetIndex++;
            _currentTarget = _path[_currentTargetIndex];
        }
    }

    private async UniTask MoveToTarget(TileData targetTile)
    {
        Vector3 target = HexCalculator.ToWorldPosition(targetTile.Coords.Q, targetTile.Coords.R, 1.7f);

        target += Vector3.up * _yOffset;
        _distanceOfClosestTargetTile = Vector3.Distance(_myTranstorm.position, target);
        while (_isMoving && _distanceOfClosestTargetTile > 0.1f)
        {
            _distanceOfClosestTargetTile = Vector3.Distance(_myTranstorm.position, target);
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

    [ContextMenu("Print()")]
    public void Print()
    {
        Debug.Log($"_remaingsPath.Count = {_remaingsPath.Count}");
    }
}
