using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Infrastructure
{
    public class PauseState : IState
    {
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
