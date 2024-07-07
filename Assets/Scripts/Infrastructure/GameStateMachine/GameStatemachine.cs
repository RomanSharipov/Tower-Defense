using System;
using System.Collections.Generic;
using VContainer;

namespace Assets.Scripts.Infrastructure
{
    public class GameStatemachine
    {
        public readonly Dictionary<Type, IState> _states;
        private IState _activeState;
        
        public GameStatemachine(Dictionary<Type, IState> states, IObjectResolver objectResolver)
        {
            _states = states;

            foreach (IState state in _states.Values)
            {
                objectResolver.Inject(state);
            }
        }

        public void Enter<TState>() where TState : IState
        {
            _activeState?.Exit();
            IState state = _states[typeof(TState)];
            _activeState = state;
            state.Enter();
        }
    }

}
