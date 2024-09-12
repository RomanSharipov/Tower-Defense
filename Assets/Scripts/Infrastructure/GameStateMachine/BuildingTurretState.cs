using Assets.Scripts.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Infrastructure
{
    public class BuildingTurretState : IState
    {
        private IBuildingService _buildingService;
        private IAppStateService _appStateService;

        [Inject]
        public BuildingTurretState(IBuildingService buildingService, IAppStateService appStateService)
        {
            _buildingService = buildingService;
            _appStateService = appStateService;
        }

        public async UniTask Enter()
        {
            await _buildingService.StartBuilding();
            _appStateService.GoToState(State.PlayingIdleState);
        }
        
        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}
