using System;
using System.Collections.Generic;
using Assets.Scripts.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure
{
    public class GameLoopState : IState
    {
        private IObjectResolver _resolver;
        private ILevelService _levelService;
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
        public void Construct(IObjectResolver objectResolver,ILevelService levelService)
        {
            _resolver = objectResolver;
            _levelService = levelService;
            foreach (IState state in _subStatemachine.States.Values)
            {
                _resolver.Inject(state);
            }
        }

        public UniTask Enter()
        {
            _levelService.LoadCurrentLevel();
            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}
