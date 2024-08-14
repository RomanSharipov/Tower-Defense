using VContainer;
using UnityEngine;
using CodeBase.Infrastructure.UI.Services;

namespace CodeBase.Infrastructure.UI
{
    public class GameLoopWindow : WindowBase
    {
        [SerializeField] private GoToStateButton[] _goToStateButton; 
        [SerializeField] private BuildButton _buildButton; 

        [Inject]
        public void Construct(IObjectResolver objectResolver)
        {
            GameRoot gameRoot = objectResolver.Resolve<GameRoot>();

            foreach (GoToStateButton button in _goToStateButton)
            {
                button.Construct(gameRoot.MainGameStatemachine,
                    gameRoot.GameLoopStatemachine);
            }
            _buildButton.Construct(gameRoot.GameLoopStatemachine);
        }
    }
}
