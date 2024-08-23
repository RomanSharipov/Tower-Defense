using UnityEngine;

public class CollisionAvoidance2 : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Vector3 boxSize = new Vector3(1f, 1f, 1f); // Размер BoxCast-а
    [SerializeField] private float detectionDistance = 2f; // Дистанция проверки

    [SerializeField] private EnemyMovement _myMovement;
    
    //private void Update()
    //{
    //    if (IsEnemyAhead(out RaycastHit hit))
    //    {
    //        if (hit.collider.gameObject.TryGetComponent(out Movement otherMovement))
    //        {
    //            if (otherMovement.DistanceOfClosestTargetTile < _myMovement.DistanceOfClosestTargetTile)
    //            {
    //                _myMovement.StopMovement();
    //                return;
    //            }
    //        }
    //    }
    //    _myMovement.StartMovement();
    //}

    private bool IsEnemyAhead(out RaycastHit hit)
    {
        // Направление движения объекта
        Vector3 direction = transform.forward;

        // Положение BoxCast-а (начальная точка)
        Vector3 origin = transform.position + Vector3.up * 0.5f; // Подняли немного вверх для проверки по центру объекта

        // Выполняем BoxCast в направлении движения
        
        



        return Physics.BoxCast(origin, boxSize / 2, direction, out hit, transform.rotation, detectionDistance, enemyLayer);


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Направление движения объекта
        Vector3 direction = transform.forward;

        // Положение BoxCast-а (начальная точка)
        Vector3 origin = transform.position + Vector3.up * 0.5f; // Подняли немного вверх для проверки по центру объекта

        // Конечная точка BoxCast-а
        Vector3 destination = origin + direction * detectionDistance;

        // Визуализация BoxCast-а
        Gizmos.matrix = Matrix4x4.TRS(origin, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.forward * detectionDistance / 2, boxSize);

        // Визуализация линии направления BoxCast-а
        Gizmos.DrawLine(origin, destination);
    }
}
