using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class RotationToAttackTransition : TransitionBase
    {
        private TurretRotation _turretRotation;

        public RotationToAttackTransition(TurretBase turret, ITurretState targetState, TurretRotation turretRotation) : base(turret, targetState)
        {
            _turretRotation = turretRotation;
        }

        public override bool ShouldTransition()
        {
            return _turretRotation.IsRotationComplete();
        }
    }
}
