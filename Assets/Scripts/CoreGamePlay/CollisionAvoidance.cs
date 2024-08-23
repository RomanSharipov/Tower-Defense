using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CollisionAvoidance : MonoBehaviour
{
    [SerializeField] private EnemyMovement myMovement;
    [SerializeField] private int count;
    [SerializeField] private bool _blockControl;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyMovement otherMovement))
        {
            count++;
            if (FurtherFromTheTargetThan(otherMovement))
            {
                myMovement.Pause();
                return;
            }
            if (SameDistanceWith(otherMovement))
            {
                if (_blockControl)
                    return;

                otherMovement.BlockTriggerOnCollisionAvoidance();

                if (FurtherFromCurrentTileThan(otherMovement))
                {
                    myMovement.Pause();
                    otherMovement.UnPause();
                }
                else 
                {
                    myMovement.UnPause();
                    otherMovement.Pause();
                }

            }
        }
    }

    public async UniTask BlockTrigger()
    {
        _blockControl = true;
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        _blockControl = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EnemyMovement otherMovement))
        {
            count--;
            if (count == 0)
            {
                myMovement.UnPause();
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
