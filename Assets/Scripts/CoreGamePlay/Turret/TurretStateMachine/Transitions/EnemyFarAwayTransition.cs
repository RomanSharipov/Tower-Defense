using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class EnemyFarAwayTransition : TransitionBase
    {
        public EnemyFarAwayTransition(TurretBase turret, ITurretState targetState) : base(turret, targetState)
        {
        }

        public override bool ShouldTransition()
        {
            if (_turret.CurrentTarget == null)
                return true;
            
            return _turret.DetectorEnemies.PointFarAway(_turret.CurrentTarget.Position);
        }
    }
}
