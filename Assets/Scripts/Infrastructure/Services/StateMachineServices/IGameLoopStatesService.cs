using System;

namespace CodeBase.Infrastructure.Services
{
    public interface IGameLoopStatesService
    {
        public void Enter<TState>() where TState : IState;
    }
}
