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
        [Inject] private readonly IPlayerHealthService _playerHealthService;

        public async UniTask Enter()
        {
            _gameStatusService.ResetStatus();
            _playerHealthService.ResetHealth();
            _wavesService.ResetWaves();
            _windowService.CloseAllWindows();
            _levelService.UnLoadCurrentLevel();
            _playerHealthService.Unsubscribe();
            _levelService.IncreaseCurrentLevel();

            ISceneInitializer levelMain = await _levelService.LoadCurrentLevel();
            levelMain.InitializeSceneServices();
            _gameLoopStatesService.Enter<PlayingIdleState>();
        }

        public UniTask Exit()
        {
            _windowService.Open(WindowId.GameLoopWindow);
            return UniTask.CompletedTask;
        }
    }
}
