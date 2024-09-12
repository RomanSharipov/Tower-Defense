using System;
using CodeBase.Infrastructure;
using UniRx;

namespace Assets.Scripts.Infrastructure.Services
{
    public interface IAppStateService
    {
        public IReadOnlyReactiveProperty<State> State { get; }
        public void GoToBuildingTurretState(Action<BuildingTurretState> setupBeforeEnter);
        public void GoToState(State state);
    }
}
