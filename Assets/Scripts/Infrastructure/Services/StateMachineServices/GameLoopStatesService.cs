using System;
using UniRx;

namespace CodeBase.Infrastructure.Services
{
    public class GameLoopStatesService : IGameLoopStatesService
    {
        private readonly GameStatemachine _gameLoopStatemachine;
        
        public GameLoopStatesService(GameStatemachine gameLoopStatemachine)
        {
            _gameLoopStatemachine = gameLoopStatemachine;
        }

        public void EnterToPauseState()
        {
            _gameLoopStatemachine.Enter<PauseState>();
        }

        public void EnterToTransitToNextLevelState()
        {
            _gameLoopStatemachine.Enter<TransitToNextLevelState>();
        }

        public void EnterToEmptyState()
        {
            _gameLoopStatemachine.Enter<EmptyState>();
        }

        public void EnterToPlayingIdleState()
        {
            _gameLoopStatemachine.Enter<PlayingIdleState>();
        }

        public void EnterToBuildingTurretState(Action<BuildingTurretState> setupBeforeEnter)
        {
            _gameLoopStatemachine.Enter(setupBeforeEnter);
        }
    }
}
