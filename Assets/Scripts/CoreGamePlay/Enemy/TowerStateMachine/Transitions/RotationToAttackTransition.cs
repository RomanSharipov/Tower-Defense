using System.Collections.Generic;

namespace Assets.Scripts.CoreGamePlay
{
    public class RotationToAttackTransition : ITransition
    {
        private TurretBase _turret;
        private ITurretState _targetState;

        public RotationToAttackTransition(TurretBase tower, ITurretState targetState)
        {
            _turret = tower;
            _targetState = targetState;
        }

        public ITurretState GetTargetState()
        {
            throw new System.NotImplementedException();
        }

        public bool ShouldTransition()
        {
            throw new System.NotImplementedException();
        }
    }
}
