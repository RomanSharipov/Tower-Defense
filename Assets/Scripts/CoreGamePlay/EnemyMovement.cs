using UnityEngine;
using System;
using Assets.Scripts.Helpers;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using Assets.Scripts.CoreGamePlay;

[Serializable]
public class EnemyMovement : MonoBehaviour
{
    private TileData[] _path;
    [SerializeField] private TileView[] _pathView;
    [SerializeField] private TileData _currentTarget;
    [SerializeField] private TileView _currentTileView;

    private Transform _myTranstorm;
    

    [SerializeField] private float _startSpeed = 2.1f;
    [SerializeField] private CollisionAvoidance _collisionAvoidance;
    private float _currentSpeed;
    private float _rotateSpeed = 360f;

    [SerializeField] private int _currentTargetIndex = 0;
    [SerializeField] private bool _isMoving = false;
    private float _yOffset = 0.41f;
    private float _distanceOfClosestTargetTile;
    
    [SerializeField] private PathBuilder _pathBuilder;
    

    public float DistanceOfClosestTargetTile => _distanceOfClosestTargetTile;


    private void Awake()
    {
        _myTranstorm = transform;
        _currentSpeed = _startSpeed;
        _pathBuilder = new PathBuilder();
    }
    
    public void BlockTriggerOnCollisionAvoidance()
    {
        _collisionAvoidance.BlockTrigger().Forget();
    }
    
    public void SetPath(TileData[] pathPoints)
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
        
        if (_pathBuilder.TryUpdatePath(tileData, _currentTargetIndex))
        {
            SetPath(_pathBuilder.Path);


            SetCurrentTarget(0);

        }

        StartMovement();
    }
    
    private void Update()
    {
        _currentTileView = _currentTarget.Tile;
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
                _pathBuilder.RemoveCompletedTile(_currentTarget);
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
}
