using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Infrastructure
{
    public class MenuState : IState
    {
        private GameStatemachine _mainGameStatemachine;

        public MenuState(GameStatemachine mainGameStatemachine)
        {
            _mainGameStatemachine = mainGameStatemachine;
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
