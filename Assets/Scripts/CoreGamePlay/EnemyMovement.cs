using UnityEngine;
using System;
using Assets.Scripts.Helpers;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using Assets.Scripts.CoreGamePlay;
using System.Collections.Generic;

[Serializable]
public class EnemyMovement : MonoBehaviour
{
    private List<TileData> _path;
    [SerializeField] private TileView[] _pathView;
    [SerializeField] private TileData _currentTarget;
    

    private Transform _myTranstorm;
    

    [SerializeField] private float _startSpeed = 2.1f;
    [SerializeField] private CollisionAvoidance _collisionAvoidance;
    private float _currentSpeed;
    private float _rotateSpeed = 360f;

    [SerializeField] private int _pathCount;
    [SerializeField] private int _currentTargetIndex = 0;
    [SerializeField] private bool _isMoving = false;
    private float _yOffset = 0.41f;
    private float _distanceOfClosestTargetTile;
    
    [SerializeField] private PathBuilder _pathBuilder;
    
    public float DistanceOfClosestTargetTile => _distanceOfClosestTargetTile;
    public TileData CurrentTarget => _currentTarget;
    public void UnPause()
    {
        DOTween.To(() => _currentSpeed, x => _currentSpeed = x, _startSpeed, 0.5f);
    }

    public void Pause()
    {
        DOTween.To(() => _currentSpeed, x => _currentSpeed = x, 0f, 0.5f);
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

    private void Awake()
    {
        _myTranstorm = transform;
        _currentSpeed = _startSpeed;
        _pathBuilder = new PathBuilder();
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
        
        if (_pathBuilder.TryUpdatePath(tileData, _currentTargetIndex,out bool buildedRightInFrontOfUs,out bool newPathOppositeDirection))
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
    
    private void Update()
    {
        if (_isMoving)
        {
            MoveAlongPath();
        }
    }

    private void MoveAlongPath()
    {
        _pathCount = _path.Count;
        if (_currentTargetIndex < _path.Count)
        {
            _currentTarget = _path[_currentTargetIndex];
            MoveToTarget(_currentTarget);

            _distanceOfClosestTargetTile = Vector3.Distance(_myTranstorm.position, GetTargetPosition(_currentTarget));

            if (_distanceOfClosestTargetTile <= 0.1f)
            {
                _pathBuilder.RemoveCompletedTile(_currentTarget);
                if (_currentTargetIndex + 1 >= _path.Count)
                {
                    StopMovement();
                }
                else
                {
                    _currentTargetIndex++;

                    if (_currentTargetIndex >= _path.Count - 1)
                    {
                        StopMovement();
                        Destroy(gameObject);
                    }
                    else
                    {
                        _currentTarget = _path[_currentTargetIndex];
                    }
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
}
