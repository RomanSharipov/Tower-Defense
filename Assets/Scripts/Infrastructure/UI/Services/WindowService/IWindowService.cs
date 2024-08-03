using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.UI.Services
{
    public interface IWindowService
    {
        public void CloseAllWindows();
        public UniTask Open(WindowId windowId);
    }
}
