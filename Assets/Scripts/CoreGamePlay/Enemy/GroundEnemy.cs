using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{

    public abstract class GroundEnemy : EnemyBase 
    {
        [SerializeField] private GroundEnemyMovement _groundEnemyMovement;

        public override IEnemyMovement EnemyMovement => _groundEnemyMovement;
        public GroundEnemyMovement GroundEnemyMovement => _groundEnemyMovement;
    }
}
