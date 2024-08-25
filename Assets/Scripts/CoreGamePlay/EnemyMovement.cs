using UnityEngine;
using System.Collections.Generic;
using Tarodev_Pathfinding._Scripts;
using System;
using Assets.Scripts.Helpers;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using Assets.Scripts.CoreGamePlay;

[Serializable]
public class EnemyMovement : MonoBehaviour
{
    private TileData[] _path;
    private TileData _currentTarget;

    private Transform _myTranstorm;
    

    [SerializeField] private float _startSpeed = 2.1f;
    [SerializeField] private CollisionAvoidance _collisionAvoidance;
    private float _currentSpeed;
    private float _rotateSpeed = 360f;

    private int _currentTargetIndex = 0;
    private bool _isMoving = false;
    private float _yOffset = 0.41f;
    private float _distanceOfClosestTargetTile;
    private HashSet<TileData> _remaingsPath = new HashSet<TileData>();
    

    public float DistanceOfClosestTargetTile => _distanceOfClosestTargetTile;
    public int RemainingTiles => _remaingsPath.Count;

    public int RemaningTiles => _remaingsPath.Count;

    private void Awake()
    {
        _myTranstorm = transform;
        _currentSpeed = _startSpeed;
    }
    
    public void BlockTriggerOnCollisionAvoidance()
    {
        _collisionAvoidance.BlockTrigger().Forget();
    }

    public void SetPath(TileData[] pathPoints)
    {
        _path = pathPoints;
        _remaingsPath.Clear();
        foreach (TileData pathPoint in _path)
        {
            _remaingsPath.Add(pathPoint);
        }
        _currentTargetIndex = 0;
        _isMoving = true;
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

    public bool PathContainsTile(TileData newUnwalkableTile)
    {
        return _remaingsPath.Contains(newUnwalkableTile);
    }

    public void UpdatePath(TileData newObstacleTile)
    {
        StopMovement();

        if (newObstacleTile == _currentTarget)
        {
            _currentTargetIndex--;
            if (_currentTargetIndex >= 0)
            {
                _currentTarget = _path[_currentTargetIndex];
            }
        }

        List<TileData> newListPath = Pathfinding.FindPath(_currentTarget, _path[_path.Length - 1]);
        newListPath.Add(_currentTarget);
        newListPath.Reverse();
        _path = newListPath.ToArray();


        SetPath(_path);
        StartMovement();
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
        if (_currentTargetIndex < _path.Length)
        {
            _currentTarget = _path[_currentTargetIndex];
            MoveToTarget(_currentTarget);

            _distanceOfClosestTargetTile = Vector3.Distance(_myTranstorm.position, GetTargetPosition(_currentTarget));

            if (_distanceOfClosestTargetTile <= 0.1f)
            {
                _remaingsPath.Remove(_currentTarget);

                if (_currentTargetIndex + 1 >= _path.Length)
                {
                    StopMovement();
                }
                else
                {
                    _currentTargetIndex++;
                    _currentTarget = _path[_currentTargetIndex];
                }
            }
        }
        else
        {
            StopMovement();
        }
    }

    private void MoveToTarget(TileData targetTile)
    {
        Vector3 target = GetTargetPosition(targetTile);

        _distanceOfClosestTargetTile = Vector3.Distance(_myTranstorm.position, target);
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
        Vector3 direction = (target - _myTranstorm.position).normalized;
        _myTranstorm.position += direction * _currentSpeed * Time.deltaTime;
    }

    private void RotateTowardsTarget(Vector3 target)
    {
        Vector3 direction = (target - _myTranstorm.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        _myTranstorm.rotation = Quaternion.RotateTowards(_myTranstorm.rotation, lookRotation, _rotateSpeed * Time.deltaTime);
    }
    
    public void UnPause()
    {
        DOTween.To(() => _currentSpeed, x => _currentSpeed = x, _startSpeed, 0.5f);
    }
    
    public void Pause()
    {
        DOTween.To(() => _currentSpeed, x => _currentSpeed = x, 0f, 0.5f);
    }

    public void UpdatePathIfNeeded(TileData tileData)
    {
        if (PathContainsTile(tileData))
        {
            UpdatePath(tileData);
        }
    }
}
