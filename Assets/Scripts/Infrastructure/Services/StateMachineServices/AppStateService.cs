using System;
using UniRx;

namespace CodeBase.Infrastructure.Services
{
    public class AppStateService : IAppStateService
    {
        private readonly GameStatemachine _mainGameStatemachine;
        
        public AppStateService(GameStatemachine mainGameStatemachine)
        {
            _mainGameStatemachine = mainGameStatemachine;
        }

        public void Enter<TState>() where TState : IState
        {
            _mainGameStatemachine.Enter<TState>();
        }
    }
}
