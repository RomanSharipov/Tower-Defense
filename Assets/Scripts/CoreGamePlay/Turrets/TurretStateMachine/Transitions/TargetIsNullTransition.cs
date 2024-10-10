using NTC.Pool;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class TargetIsNullTransition : TransitionBase
    {
        public TargetIsNullTransition(TurretBase turret, ITurretState targetState) : base(turret, targetState)
        {
        }

        public override bool ShouldTransition()
        {
            if (NightPool.GetCloneStatus(_turret.CurrentTarget) == PoolableStatus.Despawned)
                return true;

            return false;
        }
    }
}
