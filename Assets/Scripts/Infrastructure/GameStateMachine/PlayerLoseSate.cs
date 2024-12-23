using CodeBase.Infrastructure.UI.Services;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Infrastructure
{
    public class PlayerLoseSate : IState
    {
        [Inject] private IWindowService _windowService;


        public UniTask Enter()
        {
            _windowService.Open(WindowId.LoseWindow);
            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}
