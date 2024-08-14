using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class Movement
{
    private Vector3[] _pathPoints;
    private readonly Transform _myTranstorm;
    private float _moveSpeed = 5f;
    private float _rotateSpeed = 360f;

    private int _currentTargetIndex = 0;
    private bool _isMoving = false;
    private CancellationTokenSource _cancellationTokenSource;

    public Movement(Transform myTranstorm)
    {
        _myTranstorm = myTranstorm;
    }

    public void SetPath(Vector3[] pathPoints)
    {
        _pathPoints = pathPoints;
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

    private async UniTaskVoid MoveAlongPath()
    {
        while (_isMoving && _currentTargetIndex < _pathPoints.Length)
        {
            Vector3 targetPoint = _pathPoints[_currentTargetIndex];
            await MoveToTarget(targetPoint);
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
