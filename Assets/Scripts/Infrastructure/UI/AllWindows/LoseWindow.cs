using VContainer;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.UI
{
    public class LoseWindow : WindowBase
    {
        [SerializeField] private Button _restartButton;
        
        [Inject] private IGameLoopStatesService _gameLoopStatesService;

        public override void Initialize()
        {
            base.Initialize();
            _restartButton.OnClickAsObservable().Subscribe(_ =>
            {
                _gameLoopStatesService.Enter<RestartState>();
            }).AddTo(this);
        }
    }
}
