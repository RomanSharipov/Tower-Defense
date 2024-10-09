using System;
using Assets.Scripts.CoreGamePlay;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CollisionAvoidance : MonoBehaviour
{
    [SerializeField] private GroundEnemyMovement myMovement;
    [SerializeField] private int count;
    [SerializeField] private bool _blockControl;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GroundEnemy otherEnemy))
        {
            count++;
            otherEnemy.Died += OnTriggerExitHandle;
            if (otherEnemy.GroundEnemyMovement.CurrentTarget == myMovement.CurrentTarget)
            {
                if (_blockControl)
                    return;
                
                otherEnemy.GroundEnemyMovement.BlockTriggerOnCollisionAvoidance();
                ResolveByDistanceToClosestTile(otherEnemy.GroundEnemyMovement);
            }
            else
            {
                myMovement.Pause();
            }
        }
    }

    private void ResolveByDistanceToClosestTile(GroundEnemyMovement otherMovement)
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
        if (other.TryGetComponent(out EnemyBase otherEnemy))
        {
            OnTriggerExitHandle(otherEnemy);
        }
    }

    private void OnTriggerExitHandle(EnemyBase otherEnemy)
    {
        count--;
        otherEnemy.Died -= OnTriggerExitHandle;
        count = Math.Max(count, 0);

        if (count == 0)
        {
            myMovement.UnPause();
        }
    }

    private bool FurtherFromCurrentTileThan(GroundEnemyMovement otherEnemy)
    {
        return myMovement.DistanceOfClosestTargetTile > otherEnemy.DistanceOfClosestTargetTile;
    }
}
