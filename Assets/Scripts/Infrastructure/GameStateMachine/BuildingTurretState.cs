using System;
using Assets.Scripts.CoreGamePlay;
using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Infrastructure
{
    public class BuildingTurretState : IState
    {
        private IBuildingService _buildingService;
        
        private IGameLoopStatesService _gameLoopStatesService;
        private TurretId _turretId;

        [Inject]
        public BuildingTurretState(IBuildingService buildingService,IGameLoopStatesService gameLoopStatesService)
        {
            _buildingService = buildingService;
            _gameLoopStatesService = gameLoopStatesService;
        }

        public async UniTask Enter()
        {
            await _buildingService.StartBuilding(_turretId);
            _gameLoopStatesService.EnterToPlayingIdleState();
        }
        
        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }

        public void Setup(TurretId turretId)
        {
            _turretId = turretId;
        }
    }
}
