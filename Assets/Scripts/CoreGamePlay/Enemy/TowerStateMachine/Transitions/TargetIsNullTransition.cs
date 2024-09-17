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
            if (_turret.CurrentTarget == null)
                return true;

            return false;
        }
    }
}
