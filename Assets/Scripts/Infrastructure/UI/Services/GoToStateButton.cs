using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.UI.Services
{
    public class GoToStateButton : MonoBehaviour
    {
        [SerializeField] private State _targetState;
        [SerializeField] private Button _button;

        private GameStatemachine _rootGameStatemachine;
        private GameStatemachine _gameLoopStatemachine;

        public void Construct(GameStatemachine gameStatemachine,GameStatemachine gameLoopStatemachine)
        {
            _rootGameStatemachine = gameStatemachine;
            _gameLoopStatemachine = gameLoopStatemachine;
        }

        private void Awake()
        {
            _button.OnClickAsObservable().Subscribe(_ =>
            {
                switch (_targetState)
                {
                    case State.None:
                        break;
                    case State.GameLoopState:
                        _rootGameStatemachine.Enter<GameLoopState>();
                        break;
                    case State.MenuState:
                        _rootGameStatemachine.Enter<MenuState>();
                        break;
                    case State.PauseState:
                        _gameLoopStatemachine.Enter<PauseState>();
                        break;
                }
            }).AddTo(this);
        }
    }
    
    enum State
    {
        None,
        GameLoopState,
        MenuState,
        PauseState,
    }
}
