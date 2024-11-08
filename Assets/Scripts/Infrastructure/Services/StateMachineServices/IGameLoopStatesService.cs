using System;
using UniRx;

namespace CodeBase.Infrastructure.Services
{
    public interface IGameLoopStatesService
    {
        public void EnterToBuildingTurretState(Action<BuildingTurretState> setupBeforeEnter);
        public void EnterToEmptyState();
        public void EnterToPauseState();
        public void EnterToPlayingIdleState();
        public void EnterToTransitToNextLevelState();
    }
}
