using System.Collections.Generic;

namespace Assets.Scripts.CoreGamePlay
{
    public class EnemyNearbyTransition : ITransition
    {
        private TurretBase _turret;
        private ITurretState _targetState;

        public EnemyNearbyTransition(TurretBase tower, ITurretState targetState)
        {
            _turret = tower;
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
