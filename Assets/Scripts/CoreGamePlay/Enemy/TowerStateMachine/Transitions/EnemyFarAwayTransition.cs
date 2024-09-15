using System.Collections.Generic;

namespace Assets.Scripts.CoreGamePlay
{
    public class EnemyFarAwayTransition : ITransition
    {
        private TurretBase _turret;
        private ITurretState _targetState;

        public EnemyFarAwayTransition(TurretBase turret, ITurretState targetState)
        {
            _turret = turret;
            _targetState = targetState;
        }
        
        public ITurretState GetTargetState()
        {
            return _targetState;
        }

        public bool ShouldTransition()
        {
            throw new System.NotImplementedException();
        }
    }
}
