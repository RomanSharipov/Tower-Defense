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

        public void Enter<TState>(Action<TState> setupBeforeEnter = null) where TState : IState 
        {
            _gameLoopStatemachine.Enter<TState>(setupBeforeEnter);
        }

    }
}
