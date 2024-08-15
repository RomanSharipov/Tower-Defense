using Assets.Scripts.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure
{
    public class BuildingTurretState : IState
    {
        private IBuildingService _buildingService;
        private readonly GameStatemachine _gameStatemachine;

        public BuildingTurretState(GameStatemachine gameStatemachine)
        {
            _gameStatemachine = gameStatemachine;
        }

        [Inject]
        public void Construct(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        public async UniTask Enter()
        {
            await _buildingService.StartBuilding();
            _gameStatemachine.Enter<IdleState>();
        }
        
        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}
