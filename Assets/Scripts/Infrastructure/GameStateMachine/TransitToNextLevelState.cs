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
        [Inject] private readonly IGameStatusService _gameStatusService;
        [Inject] private readonly IWavesService _wavesService;

        public async UniTask Enter()
        {
            _windowService.CloseWindow(WindowId.GameLoopWindow);
            _wavesService.ResetWaves();
            _windowService.CloseWindow(WindowId.WinWindow);
            _levelService.UnLoadCurrentLevel();
            _levelService.IncreaseCurrentLevel();
            ISceneInitializer levelMain = await _levelService.LoadCurrentLevel();
            levelMain.InitializeSceneServices();
            _gameLoopStatesService.Enter<PlayingIdleState>();
        }

        public UniTask Exit()
        {
            _windowService.Open(WindowId.GameLoopWindow);
            _gameStatusService.ResetStatus();
            return UniTask.CompletedTask;
        }
    }
}
