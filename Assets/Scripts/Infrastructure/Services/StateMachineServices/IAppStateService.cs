namespace CodeBase.Infrastructure.Services
{
    public interface IAppStateService
    {
        public void EnterToBootstrapState();
        public void EnterToGameLoopState();
        public void EnterToMenuState();
    }
}
