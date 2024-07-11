using System;
using System.Collections.Generic;

namespace Assets.Scripts.Infrastructure
{
    public class GameStatemachine
    {
        private Dictionary<Type, IState> _states;
        private IState _activeState;

        public IReadOnlyDictionary<Type, IState> States => _states;
        
        public void SetStates(Dictionary<Type, IState> states)
        {
            _states = states;
        }

        public void Enter<TState>(Action<TState> setupBeforeEnter = null) where TState : IState
        {
            _activeState?.Exit();
            IState state = _states[typeof(TState)];
            _activeState = state;
            
            setupBeforeEnter?.Invoke((TState)state);

            state.Enter();
        }
    }

}
