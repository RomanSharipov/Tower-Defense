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
        
        public void EnterToBootstrapState()
        {
            _mainGameStatemachine.Enter<BootstrapState>();
        }

        public void EnterToMenuState()
        {
            _mainGameStatemachine.Enter<MenuState>();
        }

        public void EnterToGameLoopState()
        {
            _mainGameStatemachine.Enter<GameLoopState>();
        }
    }
}
