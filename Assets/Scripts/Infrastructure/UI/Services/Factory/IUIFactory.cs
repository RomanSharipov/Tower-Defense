using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.UI.Services
{
    public interface IUIFactory
    {
        public UniTask<GameLoopWindow> CreateGameLoopWindow();
        public UniTask<MainMenu> CreateMainMenu();
        public UniTask CreateRootCanvas();
        public UniTask CreateShop();
    }
}