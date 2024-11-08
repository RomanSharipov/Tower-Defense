using VContainer;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.UI
{
    public class WinWindow : WindowBase
    {
        [SerializeField] private Button _nextLevelButton;
        
        [Inject] private IGameLoopStatesService _gameLoopStatesService;

        protected override void OnAwake()
        {
            base.OnAwake();

            _nextLevelButton.OnClickAsObservable().Subscribe(_ =>
            {
                _gameLoopStatesService.EnterToTransitToNextLevelState();
            }).AddTo(this);
        }
    }
}
