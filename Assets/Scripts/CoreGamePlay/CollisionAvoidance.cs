using System;
using Assets.Scripts.CoreGamePlay;
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
            if (otherMovement.CurrentTarget == myMovement.CurrentTarget)
            {
                if (_blockControl)
                    return;
                
                otherMovement.BlockTriggerOnCollisionAvoidance();
                ResolveByDistanceToClosestTile(otherMovement);
            }
            else
            {
                myMovement.Pause();
            }
        }
    }

    private void ResolveByDistanceToClosestTile(EnemyMovement otherMovement)
    {
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

    public async UniTask BlockTrigger()
    {
        _blockControl = true;
        await UniTask.Delay(TimeSpan.FromSeconds(5.2f));
        _blockControl = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EnemyMovement otherMovement))
        {
            count--;
            count = Math.Max(count, 0);

            if (count == 0)
            {
                myMovement.UnPause();
            }
        }
    }
    
    private bool FurtherFromCurrentTileThan(EnemyMovement otherEnemy)
    {
        return myMovement.DistanceOfClosestTargetTile > otherEnemy.DistanceOfClosestTargetTile;
    }
}
