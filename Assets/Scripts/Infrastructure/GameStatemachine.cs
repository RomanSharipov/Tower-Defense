using System;
using System.Collections.Generic;

namespace Assets.Scripts.Infrastructure
{
    public class GameStatemachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;

        public GameStatemachine()
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState()
            };
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
