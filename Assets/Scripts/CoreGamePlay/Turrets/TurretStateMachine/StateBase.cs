using System.Collections.Generic;

namespace Assets.Scripts.CoreGamePlay
{
    public abstract class StateBase : ITurretState
    {
        private List<ITurretTransition> _transitions = new List<ITurretTransition>();

        protected TurretBase _turret;
        
        public StateBase(TurretBase turret)
        {
            _turret = turret;
        }

        public abstract void Enter();
        public abstract void UpdateState();
        public abstract void Exit();

        public void Update()
        {
            UpdateState();
            HandleTransitions();
        }
        
        public void AddTransitions(params ITurretTransition[] stateTransitions)
        {
            foreach (ITurretTransition transition in stateTransitions)
            {
                _transitions.Add(transition);
            }
        }

        private void HandleTransitions()
        {
            foreach (ITurretTransition transition in _transitions)
            {
                if (transition.ShouldTransition())
                {
                    _turret.TurretStateMachine.SetState(transition.TargetState);
                    return;
                }
            }
        }
    }
}
