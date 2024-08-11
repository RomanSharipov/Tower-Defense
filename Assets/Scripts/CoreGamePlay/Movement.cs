using UnityEngine;
using Cysharp.Threading.Tasks;

public class Movement 
{
    private Vector3[] _pathPoints;
    private readonly Transform _myTranstorm;
    private float moveSpeed = 5f;
    private float rotateSpeed = 360f;
    
    private int currentTargetIndex = 0;


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
        MoveAlongPath().Forget();
    }

    private async UniTaskVoid MoveAlongPath()
    {
        while (currentTargetIndex < _pathPoints.Length)
        {
            Vector3 targetPoint = _pathPoints[currentTargetIndex];
            await MoveToTarget(targetPoint);
            currentTargetIndex++;
        }
    }

    private async UniTask MoveToTarget(Vector3 target)
    {
        while (Vector3.Distance(_myTranstorm.position, target) > 0.1f)
        {
            MoveTowardsTarget(target);
            RotateTowardsTarget(target);
            await UniTask.Yield();
        }
    }

    private void MoveTowardsTarget(Vector3 target)
    {
        Vector3 direction = (target - _myTranstorm.position).normalized;
        _myTranstorm.position += direction * moveSpeed * Time.deltaTime;
    }

    private void RotateTowardsTarget(Vector3 target)
    {
        Vector3 direction = (target - _myTranstorm.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        _myTranstorm.rotation = Quaternion.RotateTowards(_myTranstorm.rotation, lookRotation, rotateSpeed * Time.deltaTime);
    }
}
