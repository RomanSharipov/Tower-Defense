using System.Collections.Generic;

namespace Assets.Scripts.CoreGamePlay
{
    public class TargetIsNullTransition : ITransition
    {
        private TurretBase _turret;
        private ITurretState _targetState;

        public TargetIsNullTransition(TurretBase turret, ITurretState targetState)
        {
            _turret = turret;
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
