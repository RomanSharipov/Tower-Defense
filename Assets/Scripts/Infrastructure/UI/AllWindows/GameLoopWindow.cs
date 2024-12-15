using VContainer;
using UnityEngine;
using CodeBase.Infrastructure.Services;
using UnityEngine.UI;
using UniRx;
using CodeBase.Infrastructure.UI.Services;
using Assets.Scripts.CoreGamePlay;
using System;
using TMPro;

namespace CodeBase.Infrastructure.UI
{
    public class GameLoopWindow : WindowBase
    {
        [SerializeField] private Button _goToMenuButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _nextWave;
        [SerializeField] private BuildButton[] _buildTurretButtons;
        [SerializeField] private TMP_Text _wavesCount;

        [Inject] private IAppStateService _appStateService;
        [Inject] private IGameLoopStatesService _gameLoopStatesService;
        [Inject] private IWavesService _wavesService;


        private void OnBuildButtonClicked(TurretId id)
        {
            _gameLoopStatesService.EnterToBuildingTurretState(state =>
            {
                state.Setup(id);
            });
        }
        
        public override void Initialize()
        {
            _goToMenuButton.OnClickAsObservable().Subscribe(_ =>
            {
                _appStateService.EnterToMenuState();
            }).AddTo(this);
            _nextWave.OnClickAsObservable().Subscribe(_ =>
            {
                _wavesService.ProceedToNextWave();
                UpdateCurrentWavesText();
            }).AddTo(this);

            _pauseButton.OnClickAsObservable().Subscribe(_ =>
            {
                _gameLoopStatesService.EnterToPauseState();
            }).AddTo(this);
            
            foreach (BuildButton buildButton in _buildTurretButtons)
            {
                buildButton.Clicked += OnBuildButtonClicked;
            }
            UpdateCurrentWavesText();
        }

        private void UpdateCurrentWavesText()
        {
            _wavesCount.text = $"{_wavesService.CurrentWaveNumber}/{_wavesService.AllWavesCount}";
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
