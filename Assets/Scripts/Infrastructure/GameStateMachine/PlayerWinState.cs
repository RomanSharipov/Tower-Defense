using CodeBase.Infrastructure.UI;
using CodeBase.Infrastructure.UI.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure
{
    public class PlayerWinState : IState
    {
        [Inject] private readonly IWindowService _windowService;
        
        public UniTask Enter()
        {
            _windowService.CloseWindowIfOpened<TurretContextMenu>();
            _windowService.Open<WinWindow>();

            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}
