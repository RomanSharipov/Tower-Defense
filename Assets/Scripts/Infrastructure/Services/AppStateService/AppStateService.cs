using System;
using UniRx;

namespace CodeBase.Infrastructure.Services
{
    public class AppStateService : IAppStateService
    {
        private readonly GameStatemachine _mainGameStatemachine;
        private readonly GameStatemachine _gameLoopStatemachine;

        private ReactiveProperty<State> _state = new ReactiveProperty<State>();

        public IReadOnlyReactiveProperty<State> State => _state;

        public AppStateService(GameStatemachine mainGameStatemachine, GameStatemachine gameLoopStatemachine)
        {
            _mainGameStatemachine = mainGameStatemachine;
            _gameLoopStatemachine = gameLoopStatemachine;
        }

        public void GoToState(State state)
        {
            switch (state)
            {
                case Services.State.None:
                    break;
                case Services.State.BootstrapState:
                    _mainGameStatemachine.Enter<BootstrapState>();
                    break;
                case Services.State.MenuState:
                    _mainGameStatemachine.Enter<MenuState>();
                    break;
                case Services.State.GameLoopState:
                    _mainGameStatemachine.Enter<GameLoopState>();
                    break;
                case Services.State.PlayingIdleState:
                    _gameLoopStatemachine.Enter<PlayingIdleState>();
                    break;
                case Services.State.PauseState:
                    _gameLoopStatemachine.Enter<PauseState>();
                    break;
                case Services.State.BuildingTurretState:
                    _gameLoopStatemachine.Enter<BuildingTurretState>();
                    break;
                case Services.State.TransitToNextLevelState:
                    _gameLoopStatemachine.Enter<TransitToNextLevelState>();
                    break;
                default:
                    break;
            }
            _state.Value = state;
        }

        public void GoToBuildingTurretState(Action<BuildingTurretState> setupBeforeEnter)
        {
            _gameLoopStatemachine.Enter(setupBeforeEnter);
            _state.Value = Services.State.BuildingTurretState;
        }
    }
}
