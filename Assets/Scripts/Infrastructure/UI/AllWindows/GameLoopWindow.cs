using VContainer;
using UnityEngine;
using CodeBase.Infrastructure.UI.Services;
using UnityEngine.UI;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.CoreGamePlay;

namespace CodeBase.Infrastructure.UI
{
    public class GameLoopWindow : WindowBase
    {
        [SerializeField] private GoToStateButton[] _goToStateButton; 
        [SerializeField] private BuildButton _buildButton;
        [Inject] private IWavesService _wavesService; 

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

        public void TryGetEnemy()
        {
            _wavesService.TryGetEnemy(out EnemyType enemyType);
            Debug.Log($"enemyType = {enemyType}");
        }
    }
}
