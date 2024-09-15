using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class RotationToAttackTransition : TransitionBase
    {
        public RotationToAttackTransition(TurretBase turret, ITurretState targetState) : base(turret, targetState)
        {
        }

        public override bool ShouldTransition()
        {
            Debug.Log("RotationToAttackTransition ShouldTransition");
            return false;
        }
    }
}
