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
        private readonly IObjectResolver _resolver;
        private readonly ILevelService _levelService;
        private readonly IWindowService _windowService;
        private readonly IAssetProvider _assetProvider;
        private readonly IAppStateService _appStateService;

        [Inject]
        public GameLoopState(IObjectResolver resolver, 
            ILevelService levelService, IWindowService windowService, 
            IAssetProvider assetProvider, IAppStateService appStateService)
        {
            _resolver = resolver;
            _levelService = levelService;
            _windowService = windowService;
            _assetProvider = assetProvider;
            _appStateService = appStateService;
        }
        
        public async UniTask Enter()
        {
            _windowService.Open(WindowId.GameLoopWindow).Forget();
            
            ILevelMain levelMain = await _levelService.LoadCurrentLevel();
            levelMain.InitializeSceneServices();
            _appStateService.GoToState(State.PlayingIdleState);
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
