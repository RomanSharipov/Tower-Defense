using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.UI.Services
{
    public interface IUIFactory
    {
        public UniTask CreateRootCanvas();
        public UniTask CreateShop();
    }
}