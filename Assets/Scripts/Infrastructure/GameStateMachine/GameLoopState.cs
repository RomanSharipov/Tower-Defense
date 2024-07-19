using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.Infrastructure
{
    public class GameLoopState : IState
    {
        private IObjectResolver _resolver;
        private GameStatemachine _mainGameStateMachine;

        private GameStatemachine _subStatemachine;

        public GameLoopState(GameStatemachine mainGameStateMachine)
        {
            _mainGameStateMachine = mainGameStateMachine;

            _subStatemachine = new GameStatemachine();
            
            Dictionary<Type, IState> states = new Dictionary<Type, IState>()
            {
                [typeof(PauseState)] = new PauseState()
            };

            _subStatemachine.SetStates(states);
            
        }

        [Inject]
        public void Construct(IObjectResolver objectResolver)
        {
            _resolver = objectResolver;
            
            foreach (IState state in _subStatemachine.States.Values)
            {
                _resolver.Inject(state);
            }
        }

        public UniTask Enter()
        {
            Debug.Log($"GameLoopState Enter");
            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}
