using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Infrastructure
{
    public class EmptyState : IState
    {
        [Inject]
        public EmptyState()
        {
            
        }

        public UniTask Enter()
        {
            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}
