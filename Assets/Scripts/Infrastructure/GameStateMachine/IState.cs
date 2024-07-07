using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Infrastructure
{
    public interface IState
    {
        public UniTask Enter();
        public UniTask Exit();
    }
}
