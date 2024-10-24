using VContainer;
using UnityEngine;
using CodeBase.Infrastructure.Services;
using UnityEngine.UI;
using UniRx;
using CodeBase.Infrastructure.UI.Services;
using Assets.Scripts.CoreGamePlay;
using System;

namespace CodeBase.Infrastructure.UI
{
    public class GameLoopWindow : WindowBase
    {
        [SerializeField] private Button _goToMenuButton;
        [SerializeField] private BuildButton[] _buildTurretButtons;

        [Inject] private IAppStateService _appStateService;


        private void OnBuildButtonClicked(TurretId id)
        {
            _appStateService.GoToBuildingTurretState(state =>
            {
                state.Setup(id);
            });
        }
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _goToMenuButton.OnClickAsObservable().Subscribe(_ =>
            {
                _appStateService.GoToState(State.MenuState);
            }).AddTo(this);

            foreach (BuildButton buildButton in _buildTurretButtons)
            {
                buildButton.Clicked += OnBuildButtonClicked;
            }
        }

        private void OnDestroy()
        {
            foreach (BuildButton buildButton in _buildTurretButtons)
            {
                buildButton.Clicked -= OnBuildButtonClicked;
            }
        }
    }
}
