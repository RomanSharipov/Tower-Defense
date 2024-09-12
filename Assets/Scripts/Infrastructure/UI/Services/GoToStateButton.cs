using Assets.Scripts.Infrastructure.Services;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace CodeBase.Infrastructure.UI.Services
{
    public class GoToStateButton : MonoBehaviour
    {
        [SerializeField] private State _targetState;
        [SerializeField] private Button _button;

        [Inject] IAppStateService _appStateService;
        
        private void Awake()
        {
            _button.OnClickAsObservable().Subscribe(_ =>
            {
                _appStateService.GoToState(_targetState);
            }).AddTo(this);
        }
    }
}
