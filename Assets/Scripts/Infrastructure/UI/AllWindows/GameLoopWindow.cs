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
        [SerializeField] private Button _pauseButton;
        [SerializeField] private BuildButton[] _buildTurretButtons;

        [Inject] private IAppStateService _appStateService;
        [Inject] private IGameLoopStatesService _gameLoopStatesService;


        private void OnBuildButtonClicked(TurretId id)
        {
            _gameLoopStatesService.EnterToBuildingTurretState(state =>
            {
                state.Setup(id);
            });
        }
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _goToMenuButton.OnClickAsObservable().Subscribe(_ =>
            {
                _appStateService.EnterToMenuState();
            }).AddTo(this);

            _pauseButton.OnClickAsObservable().Subscribe(_ =>
            {
                _gameLoopStatesService.EnterToPauseState();
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
