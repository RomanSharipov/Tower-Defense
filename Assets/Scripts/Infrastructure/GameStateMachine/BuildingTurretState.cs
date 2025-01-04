using System;
using Assets.Scripts.CoreGamePlay;
using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Infrastructure
{
    public class BuildingTurretState : IState
    {
        [Inject] private IBuildingService _buildingService;
        [Inject] private IPlayerResourcesService _playerResourcesService;
        [Inject] private IGameLoopStatesService _gameLoopStatesService;
        [Inject] private ITurretPriceProvider _turretPriceProvider;

        private TurretId _turretId;
        
        public async UniTask Enter()
        {
            _buildingService.TurretIsBuilded += OnTurretIsBuilded;
            await _buildingService.StartBuilding(_turretId);
            _gameLoopStatesService.Enter<PlayingIdleState>();
        }

        private void OnTurretIsBuilded(TurretBase turret)
        {
            _playerResourcesService.DecreaseValue(ResourcesType.Money, _turretPriceProvider.GetPrice(_turretId));
        }

        public UniTask Exit()
        {
            _buildingService.TurretIsBuilded -= OnTurretIsBuilded;
            return UniTask.CompletedTask;
        }

        public void Setup(TurretId turretId)
        {
            _turretId = turretId;
        }
    }
}
