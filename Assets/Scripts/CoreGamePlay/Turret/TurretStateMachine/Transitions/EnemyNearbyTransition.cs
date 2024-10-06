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
            if (_turret.DetectorEnemies.TryFindEnemy(out EnemyBase totalEnemy))
            {
                _turret.CurrentTarget = totalEnemy;
                return true;
            }
            return false;
        }
    }
}
