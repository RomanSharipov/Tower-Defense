using Assets.Scripts.CoreGamePlay;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.UI.Services;
using Cysharp.Threading.Tasks;
using VContainer;
using UniRx;
using UnityEngine;
using CodeBase.Infrastructure.UI;
using System;

namespace CodeBase.Infrastructure
{
    public class GameLoopState : IState
    {
        [Inject] private readonly ILevelService _levelService;
        [Inject] private readonly IWindowService _windowService;
        [Inject] private readonly IAssetProvider _assetProvider;
        [Inject] private readonly IGameLoopStatesService _gameLoopStatesService;
        [Inject] private readonly IGameStatusService _gameStatusService;
        [Inject] private readonly IClickOnTurretTracker _clickOnTurretTracker;
        

        private CompositeDisposable _compositeDisposable = new();

        public async UniTask Enter()
        {
            _compositeDisposable.Clear();
            
            _gameStatusService.GameStatus
                
                .Subscribe(OnGameStatusChange)
                .AddTo(_compositeDisposable);
            
            _clickOnTurretTracker.ClickOnTurret
                .Subscribe(OnClickOnTurret)
                .AddTo(_compositeDisposable);
            
            _clickOnTurretTracker.StartTracking();
            _windowService.Open(WindowId.GameLoopWindow).Forget();
            
            ISceneInitializer levelMain = await _levelService.LoadCurrentLevel();
            levelMain.InitializeSceneServices();
            _gameLoopStatesService.Enter<PlayingIdleState>();
        }

        private void OnGameStatusChange(GameStatus status)
        {
            switch (status)
            {
                case GameStatus.None:
                    break;
                case GameStatus.Win:
                    _gameLoopStatesService.Enter<PlayerWinState>();
                    break;
                case GameStatus.Lose:
                    _gameLoopStatesService.Enter<PlayerLoseSate>();
                    break;
            }
        }
        
        private void OnClickOnTurret(TurretBase turret)
        {
            _windowService.CloseWindowIfOpened(WindowId.TurretContextMenu);
            _windowService.Open<TurretContextMenu>(WindowId.TurretContextMenu, window =>
            {
                window.Setup(turret);
            }).Forget();
        }
        
        public UniTask Exit()
        {
            _clickOnTurretTracker.EndTracking();
            _windowService.CloseAllWindows();
            _levelService.UnLoadCurrentLevel();
            _assetProvider.Cleanup();
            _gameLoopStatesService.Enter<EmptyState>();
            return UniTask.CompletedTask;
        }
    }
}
