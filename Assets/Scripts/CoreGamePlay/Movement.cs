using UnityEngine;
using Cysharp.Threading.Tasks;

public class Movement : MonoBehaviour
{
    [SerializeField] private Transform[] pathPoints;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 360f;

    private int currentTargetIndex = 0;

    private void StartMovement()
    {
        MoveAlongPath().Forget();
    }

    private async UniTaskVoid MoveAlongPath()
    {
        while (currentTargetIndex < pathPoints.Length)
        {
            Transform targetPoint = pathPoints[currentTargetIndex];
            await MoveToTarget(targetPoint);
            currentTargetIndex++;
        }
    }

    private async UniTask MoveToTarget(Transform target)
    {
        while (Vector3.Distance(transform.position, target.position) > 0.1f)
        {
            MoveTowardsTarget(target);
            RotateTowardsTarget(target);
            await UniTask.Yield();
        }
    }

    private void MoveTowardsTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void RotateTowardsTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
    }
}
