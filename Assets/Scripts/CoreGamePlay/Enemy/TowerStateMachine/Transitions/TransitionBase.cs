using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public abstract class TransitionBase : ITurretTransition
    {
        protected TurretBase _turret;
        protected LayerMask _enemyLayerMask;

        private ITurretState _targetState;

        public ITurretState TargetState => _targetState;

        public abstract bool ShouldTransition();

        public TransitionBase(TurretBase turret, ITurretState targetState)
        {
            _turret = turret;
            _targetState = targetState;
            _enemyLayerMask = LayerMask.GetMask("Enemy");
        }
    }
}
