using CodeBase.Infrastructure.UI;
using CodeBase.Infrastructure.UI.Services;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Infrastructure
{
    public class PlayerLoseState : IState
    {
        [Inject] private IWindowService _windowService;


        public UniTask Enter()
        {
            _windowService.Open<LoseWindow>();
            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}
