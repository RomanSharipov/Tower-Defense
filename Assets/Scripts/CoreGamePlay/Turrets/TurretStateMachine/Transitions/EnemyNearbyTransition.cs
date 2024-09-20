using System;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class EnemyNearbyTransition : TransitionBase
    {
        public EnemyNearbyTransition(TurretBase turret, ITurretState targetState) : base(turret, targetState)
        {
            
        }
        
        public override bool ShouldTransition()
        {
            return _turret.DetectorEnemies.TryFindEnemy();
        }
    }
}
