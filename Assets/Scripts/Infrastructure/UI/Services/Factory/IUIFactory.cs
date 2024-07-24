using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Infrastructure.UI
{
    public interface IUIFactory
    {
        public UniTask CreateRootCanvas();
        public UniTask CreateShop();
    }
}