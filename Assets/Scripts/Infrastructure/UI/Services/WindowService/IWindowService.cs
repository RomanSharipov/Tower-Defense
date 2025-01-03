using System;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.UI.Services
{
    public interface IWindowService
    {
        public UniTask Open<TWindow>(Action<TWindow> setupBeforeOpen = null) where TWindow : WindowBase;
        public void CloseWindow<TWindow>() where TWindow : WindowBase;
        public bool NowIsOpen<TWindow>();
        public void CloseWindowIfOpened<TWindow>() where TWindow : WindowBase;
        public void CloseAllWindows();
    }
}
