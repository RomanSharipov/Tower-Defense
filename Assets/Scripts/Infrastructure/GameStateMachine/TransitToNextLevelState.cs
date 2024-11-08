using Assets.Scripts.CoreGamePlay;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.UI.Services;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Infrastructure
{
    public class TransitToNextLevelState : IState
    {
        [Inject] private readonly ILevelService _levelService;
        [Inject] private readonly IGameLoopStatesService _gameLoopStatesService;
        [Inject] private readonly IWindowService _windowService;
        [Inject] private readonly IAssetProvider _assetProvider;

        public async UniTask Enter()
        {
            _windowService.CloseWindow(WindowId.WinWindow);
            _levelService.UnLoadCurrentLevel();
            _assetProvider.Cleanup();
            _levelService.IncreaseCurrentLevel();
            ILevelMain levelMain = await _levelService.LoadCurrentLevel();
            levelMain.InitializeSceneServices();
            _gameLoopStatesService.EnterToPlayingIdleState();
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}
