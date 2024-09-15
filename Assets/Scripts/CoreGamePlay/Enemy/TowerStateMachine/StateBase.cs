using System.Collections.Generic;

namespace Assets.Scripts.CoreGamePlay
{
    public abstract class StateBase : ITurretState
    {
        private List<ITransition> _transitions = new List<ITransition>();

        protected TurretBase _turret;
        
        public StateBase(TurretBase turret)
        {
            _turret = turret;
        }

        public abstract void Enter();
        public abstract void UpdateStateLogic();
        public abstract void Exit();

        public void Update()
        {
            UpdateStateLogic();
            HandleTransitions();
        }
        
        public void AddTransitions(params ITransition[] stateTransitions)
        {
            foreach (ITransition transition in stateTransitions)
            {
                _transitions.Add(transition);
            }
        }

        private void HandleTransitions()
        {
            foreach (ITransition transition in _transitions)
            {
                if (transition.ShouldTransition())
                {
                    _turret.TurretStateMachine.SetState(transition.GetTargetState());
                    return;
                }
            }
        }
    }
}
