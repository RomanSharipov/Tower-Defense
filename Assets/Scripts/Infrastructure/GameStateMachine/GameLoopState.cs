using Assets.Scripts.CoreGamePlay;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.UI.Services;
using Cysharp.Threading.Tasks;
using VContainer;
using UniRx;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameLoopState : IState
    {
        [Inject] private readonly ILevelService _levelService;
        [Inject] private readonly IWindowService _windowService;
        [Inject] private readonly IAssetProvider _assetProvider;
        [Inject] private readonly IGameLoopStatesService _gameLoopStatesService;
        [Inject] private readonly IPlayerWinTracker _playerWinTracker;
        [Inject] private readonly IClickOnTurretTracker _clickOnTurretTracker;

        private CompositeDisposable _compositeDisposable = new();

        public async UniTask Enter()
        {
            _compositeDisposable.Clear();
            _playerWinTracker.PlayerWon += OnPlayerWon;
            
            _clickOnTurretTracker.ClickOnTurret
                .Subscribe(OnClickOnTurret)
                .AddTo(_compositeDisposable);
            
            _clickOnTurretTracker.StartTracking();
            _windowService.Open(WindowId.GameLoopWindow).Forget();
            
            ILevelMain levelMain = await _levelService.LoadCurrentLevel();
            levelMain.InitializeSceneServices();
            _gameLoopStatesService.EnterToPlayingIdleState();
        }

        private void OnClickOnTurret(TurretBase turret)
        {
            _windowService.Open(WindowId.TurretContextMenu).Forget();
        }

        private void OnPlayerWon()
        {
            _windowService.Open(WindowId.WinWindow);
        }

        public UniTask Exit()
        {
            _clickOnTurretTracker.EndTracking();
            _windowService.CloseWindowIfOpened(WindowId.TurretContextMenu);

            _playerWinTracker.PlayerWon -= OnPlayerWon;
            _windowService.CloseWindow(WindowId.GameLoopWindow);
            _levelService.UnLoadCurrentLevel();
            _assetProvider.Cleanup();
            _gameLoopStatesService.EnterToEmptyState();
            return UniTask.CompletedTask;
        }
    }
}
