using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{

    public abstract class FlyEnemy : EnemyBase 
    {
        [SerializeField] private FlyEnemyMovement _flyEnemyMovement;

        public override IEnemyMovement EnemyMovement => _flyEnemyMovement;
    }
}
