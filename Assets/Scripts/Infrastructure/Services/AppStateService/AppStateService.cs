using System;
using CodeBase.Infrastructure;
using UniRx;

namespace Assets.Scripts.Infrastructure.Services
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
                case CodeBase.Infrastructure.State.None:
                    break;
                case CodeBase.Infrastructure.State.BootstrapState:
                    _mainGameStatemachine.Enter<BootstrapState>();
                    break;
                case CodeBase.Infrastructure.State.MenuState:
                    _mainGameStatemachine.Enter<MenuState>();
                    break;
                case CodeBase.Infrastructure.State.GameLoopState:
                    _mainGameStatemachine.Enter<GameLoopState>();
                    break;
                case CodeBase.Infrastructure.State.PlayingIdleState:
                    _gameLoopStatemachine.Enter<PlayingIdleState>();
                    break;
                case CodeBase.Infrastructure.State.PauseState:
                    _gameLoopStatemachine.Enter<PauseState>();
                    break;
                case CodeBase.Infrastructure.State.BuildingTurretState:
                    _gameLoopStatemachine.Enter<BuildingTurretState>();
                    break;
                default:
                    break;
            }
            _state.Value = state;
        }

        public void GoToBuildingTurretState(Action<BuildingTurretState> setupBeforeEnter)
        {
            _gameLoopStatemachine.Enter<BuildingTurretState>(setupBeforeEnter);
            _state.Value = CodeBase.Infrastructure.State.BuildingTurretState;
        }
    }
}
