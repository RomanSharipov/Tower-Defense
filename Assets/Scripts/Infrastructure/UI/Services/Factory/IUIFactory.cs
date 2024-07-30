using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.UI.Services
{
    public interface IUIFactory
    {
        public void DestroyAllWindows();
        public UniTask CreateMainMenu();
        public UniTask CreateRootCanvas();
        public UniTask CreateShop();
    }
}