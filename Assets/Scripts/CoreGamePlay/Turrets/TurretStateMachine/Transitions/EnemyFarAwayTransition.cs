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
            return _turret.DetectorEnemies.EnemyFarAway();
        }
    }
}
