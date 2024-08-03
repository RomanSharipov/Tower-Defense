using System;
using System.Collections.Generic;
using Assets.Scripts.Infrastructure.Services;
using CodeBase.Infrastructure.UI.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure
{
    public class GameLoopState : IState
    {
        private IObjectResolver _resolver;
        private ILevelService _levelService;
        private IWindowService _windowService;
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
        public void Construct(IObjectResolver objectResolver,ILevelService levelService, IWindowService windowService)
        {
            _resolver = objectResolver;
            _levelService = levelService;
            _windowService = windowService;
            foreach (IState state in _subStatemachine.States.Values)
            {
                _resolver.Inject(state);
            }
        }

        public UniTask Enter()
        {
            _levelService.LoadCurrentLevel();
            _windowService.Open(WindowId.GameLoopWindow);
            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            _windowService.CloseWindow(WindowId.GameLoopWindow);
            return UniTask.CompletedTask;
        }
    }
}
