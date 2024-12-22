namespace CodeBase.Infrastructure.Services
{
    public interface IAppStateService
    {
        public void Enter<TState>() where TState : IState;
    }
}
