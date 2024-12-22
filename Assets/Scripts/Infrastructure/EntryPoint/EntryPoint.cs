using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.Infrastructure
{
    public class EntryPoint : IStartable
    {
        private readonly IAppStateService _appStateService;
        
        [Inject]
        public EntryPoint(IAppStateService appStateService)
        {
            _appStateService = appStateService;
        }

        void IStartable.Start()
        {
            _appStateService.Enter<BootstrapState>();
        }
    }
}
