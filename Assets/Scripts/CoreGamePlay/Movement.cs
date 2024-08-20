using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using Assets.Scripts.Helpers;
using System.Collections.Generic;
using Tarodev_Pathfinding._Scripts;
using System;
using Assets.Scripts.CoreGamePlay;

[Serializable]
public class Movement : MonoBehaviour
{
    [SerializeField] private TileData[] _path;
    [SerializeField] private List<TileView> _pathTileView = new List<TileView>();
    private TileData _currentTarget;
    [SerializeField] private TileView _currentTileData;
    private Transform _myTranstorm;
    private string _name;
    private float _moveSpeed = 2.0f;
    private float _rotateSpeed = 360f;
    

    [SerializeField] private int _currentTargetIndex = 0;
    private bool _isMoving = false;
    private CancellationTokenSource _cancellationTokenSource;
    private float _yOffset = 0.41f;
    private HashSet<TileData> _remaingsPath = new HashSet<TileData>();

    public void NewMovement(Transform myTranstorm, string name)
    {
        _myTranstorm = myTranstorm;
        _name = name;
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
        bool contains = _remaingsPath.Contains(newUnwalkableTile);
        
        if (contains)
        {
            StopMovement();
            
            Vector3 targetPoint = HexCalculator.ToWorldPosition(_currentTarget.Coords.Q, _currentTarget.Coords.R, 1.7f);
            await MoveToTarget(targetPoint + Vector3.up * _yOffset);
            StopMovement();


            //TileData[] newPath = Pathfinding.FindPath(_currentTarget, _path[_path.Length - 1]).ToArray();
            TileData[] newPath;
            List<TileData> newListPath = Pathfinding.FindPath(_currentTarget, _path[_path.Length - 1]);
            newListPath.Add(_currentTarget);
            newListPath.Reverse();
            newPath = newListPath.ToArray();
            
            
            SetPath (newPath);
            _currentTargetIndex = 0;
            
            
            StartMovement();
            return;
        }
    }

    private async UniTaskVoid MoveAlongPath() 
    {
        while (_isMoving && _currentTargetIndex < _path.Length)
        {
            _currentTarget = _path[_currentTargetIndex];
            _currentTileData = _currentTarget.Tile;
            Vector3 targetPoint = HexCalculator.ToWorldPosition(_currentTarget.Coords.Q, _currentTarget.Coords.R,1.7f);
            await MoveToTarget(targetPoint + Vector3.up * _yOffset);
            _remaingsPath.Remove(_currentTarget);
            
            if (_currentTargetIndex + 1 >= _path.Length)
            {
                StopMovement();
                return;
            }
            
            _currentTargetIndex++;
            _currentTarget = _path[_currentTargetIndex];
            _currentTileData = _currentTarget.Tile;
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

    public void PrintRemainingTiles()
    {
        foreach (TileData item in _remaingsPath)
        {
            Debug.Log($"RemainingTiles = {item.Coords.Q}|{item.Coords.R}");
        }
    }

    public void PrintAllTiles()
    {
        foreach (TileData item in _path)
        {
            Debug.Log($"AllTiles = {item.Coords.Q}|{item.Coords.R}");
        }
    }
    public void Contains(TileData tileData)
    {
        Debug.Log($"_remaingsPath.Contains(tileData); = {_remaingsPath.Contains(tileData)}");
    }

    [ContextMenu("SetRemainingPath()")]
    public void SetRemainingPath()
    {
        _pathTileView.Clear();
        foreach (TileData item in _remaingsPath)
        {
            _pathTileView.Add(item.Tile);
        }
    }
    [ContextMenu("SetAllPath()")]
    public void SetAllPath()
    {
        _pathTileView.Clear();
        foreach (TileData item in _path)
        {
            _pathTileView.Add(item.Tile);
        }
    }

    [ContextMenu("Set_currentTarget()")]
    public void Set_currentTarget()
    {
        _currentTarget = _path[_currentTargetIndex];
    }
}
