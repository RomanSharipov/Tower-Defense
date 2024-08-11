using UnityEngine;
using Cysharp.Threading.Tasks;

public class Movement 
{
    private Transform[] _pathPoints;
    private readonly Transform _myTranstorm;
    private float moveSpeed = 5f;
    private float rotateSpeed = 360f;
    
    private int currentTargetIndex = 0;


    public Movement(Transform myTranstorm)
    {
        _myTranstorm = myTranstorm;
    }

    public void SetPath(Transform[] pathPoints)
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
            Transform targetPoint = _pathPoints[currentTargetIndex];
            await MoveToTarget(targetPoint);
            currentTargetIndex++;
        }
    }

    private async UniTask MoveToTarget(Transform target)
    {
        while (Vector3.Distance(_myTranstorm.position, target.position) > 0.1f)
        {
            MoveTowardsTarget(target);
            RotateTowardsTarget(target);
            await UniTask.Yield();
        }
    }

    private void MoveTowardsTarget(Transform target)
    {
        Vector3 direction = (target.position - _myTranstorm.position).normalized;
        _myTranstorm.position += direction * moveSpeed * Time.deltaTime;
    }

    private void RotateTowardsTarget(Transform target)
    {
        Vector3 direction = (target.position - _myTranstorm.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        _myTranstorm.rotation = Quaternion.RotateTowards(_myTranstorm.rotation, lookRotation, rotateSpeed * Time.deltaTime);
    }
}
