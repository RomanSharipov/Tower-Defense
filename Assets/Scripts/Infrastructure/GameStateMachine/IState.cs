using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure
{
    public interface IState
    {
        public UniTask Enter();
        public UniTask Exit();
    }
}
