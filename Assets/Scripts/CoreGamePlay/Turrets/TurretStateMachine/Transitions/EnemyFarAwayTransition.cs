using System.Collections.Generic;
using NTC.Pool;
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
            if (NightPool.GetCloneStatus(_turret.CurrentTarget) == PoolableStatus.Despawned)
                return true;

            return _turret.DetectorEnemies.PointFarAway(_turret.CurrentTarget.Position);
        }
    }
}
