using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameStatemachine
    {
        private Dictionary<Type, IState> _states = new Dictionary<Type, IState>();
        private IState _activeState;
        
        public void AddState(Type type,IState state)
        {
            _states.Add(type,state);
        }

        public void Enter<TState>(Action<TState> setupBeforeEnter = null) where TState : IState
        {
            if (_activeState != null && _activeState.GetType() == typeof(TState))
            {
                Debug.LogError($"State {typeof(TState).Name} is already active.");
                return;
            }
            _activeState?.Exit();
            IState state = _states[typeof(TState)];
            _activeState = state;
            
            setupBeforeEnter?.Invoke((TState)state);
            
            state.Enter();
        }
    }

}
