using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.UI.Services
{
    public class GoToStateButton : MonoBehaviour
    {
        [SerializeField] private State _targetState;
        [SerializeField] private Button _button;

        private GameStatemachine _gameStatemachine;

        public void Construct(GameStatemachine gameStatemachine)
        {
            _gameStatemachine = gameStatemachine;
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
                        _gameStatemachine.Enter<GameLoopState>();
                        break;
                    case State.MenuState:
                        _gameStatemachine.Enter<MenuState>();
                        break;
                    case State.PauseState:
                        _gameStatemachine.Enter<GameLoopState>();
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
        PauseState
    }
}
