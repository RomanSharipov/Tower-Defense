using System;

namespace CodeBase.Infrastructure.Services
{
    public interface IGameLoopStatesService
    {
        public void Enter<TState>(Action<TState> setupBeforeEnter = null) where TState : IState;
    }
}
