using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.UI.Services
{
    public interface IUIFactory
    {
        UniTask CreateMainMenu();
        public UniTask CreateRootCanvas();
        public UniTask CreateShop();
    }
}