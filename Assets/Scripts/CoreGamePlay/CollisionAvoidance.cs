using UnityEngine;

public class CollisionAvoidance : MonoBehaviour
{
    [SerializeField] private EnemyMovement myMovement;
    [SerializeField] private int count;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyMovement otherMovement))
        {
            count++;
            if (FurtherFromTheTargetThan(otherMovement))
            {
                myMovement.StopMovement();
                return;
            }
            if (SameDistanceWith(otherMovement))
            {
                if (FurtherFromCurrentTileThan(otherMovement))
                {
                    myMovement.StopMovement();
                    return;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EnemyMovement otherMovement))
        {
            count--;
            if (count == 0)
            {
                myMovement.StartMovement();
            }
        }
    }

    private bool FurtherFromTheTargetThan(EnemyMovement otherEnemy)
    {
        return myMovement.RemaningTiles > otherEnemy.RemaningTiles;
    }
    
    private bool SameDistanceWith(EnemyMovement otherEnemy)
    {
        return myMovement.RemaningTiles == otherEnemy.RemaningTiles;
    }

    private bool FurtherFromCurrentTileThan(EnemyMovement otherEnemy)
    {
        return myMovement.DistanceOfClosestTargetTile > otherEnemy.DistanceOfClosestTargetTile;
    }
}
