using System;
using System.Collections.Generic;
using Assets.Scripts.CoreGamePlay;
using Assets.Scripts.Infrastructure.Services;
using CodeBase.Infrastructure.Services;
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
        private IAssetProvider _assetProvider;
        private GameStatemachine _mainGameStateMachine;

        private GameStatemachine _subStatemachine;

        public GameStatemachine SubStatemachine => _subStatemachine;

        public GameLoopState(GameStatemachine mainGameStateMachine)
        {
            _mainGameStateMachine = mainGameStateMachine;

            _subStatemachine = new GameStatemachine();

            PauseState pauseState = new PauseState();
            BuildingTurretState buildingTurretState = new BuildingTurretState(_subStatemachine);
            IdleState idleState = new IdleState();
            
            Dictionary<Type, IState> states = new Dictionary<Type, IState>()
            {
                [typeof(PauseState)] = pauseState,
                [typeof(BuildingTurretState)] = buildingTurretState,
                [typeof(IdleState)] = idleState
            };

            _subStatemachine.SetStates(states);

            _subStatemachine.Enter<IdleState>();
        }

        [Inject]
        public void Construct(IObjectResolver objectResolver,ILevelService levelService, IWindowService windowService, IAssetProvider assetProvider)
        {
            _resolver = objectResolver;
            _levelService = levelService;
            _windowService = windowService;
            _assetProvider = assetProvider;
            foreach (IState state in _subStatemachine.States.Values)
            {
                _resolver.Inject(state);
            }
        }

        public async UniTask Enter()
        {
            _windowService.Open(WindowId.GameLoopWindow).Forget();

            ILevelMain levelMain = await _levelService.LoadCurrentLevel();
            levelMain.InitializeSceneServices();
        }

        public UniTask Exit()
        {
            _windowService.CloseWindow(WindowId.GameLoopWindow);
            _levelService.UnLoadCurrentLevel();
            _assetProvider.Cleanup();
            return UniTask.CompletedTask;
        }
    }
}
