using VContainer;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.UI
{
    public class MainMenu : WindowBase
    {
        [SerializeField] private Button _startGameButton;
        [Inject] private IAppStateService _appStateService;

        protected override void OnAwake()
        {
            base.OnAwake();
            _startGameButton.OnClickAsObservable().Subscribe(_ =>
            {
                _appStateService.Enter<GameLoopState>();
            }).AddTo(this);
        }
    }
}
