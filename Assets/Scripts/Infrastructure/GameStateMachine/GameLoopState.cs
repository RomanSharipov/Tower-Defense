using System;
using Assets.Scripts.CoreGamePlay;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.UI.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure
{
    public class GameLoopState : IState
    {
        [Inject] private readonly ILevelService _levelService;
        [Inject] private readonly IWindowService _windowService;
        [Inject] private readonly IAssetProvider _assetProvider;
        [Inject] private readonly IAppStateService _appStateService;
        [Inject] private readonly IPlayerWinTracker _playerWinTracker;
        
        public async UniTask Enter()
        {
            _playerWinTracker.PlayerWon += OnPlayerWon;

            _windowService.Open(WindowId.GameLoopWindow).Forget();
            
            ILevelMain levelMain = await _levelService.LoadCurrentLevel();
            levelMain.InitializeSceneServices();
            _appStateService.GoToState(State.PlayingIdleState);
        }

        private void OnPlayerWon()
        {
            _windowService.Open(WindowId.WinWindow);
        }

        public UniTask Exit()
        {
            _playerWinTracker.PlayerWon -= OnPlayerWon;
            _windowService.CloseWindow(WindowId.GameLoopWindow);
            _levelService.UnLoadCurrentLevel();
            _assetProvider.Cleanup();
            return UniTask.CompletedTask;
        }
    }
}
