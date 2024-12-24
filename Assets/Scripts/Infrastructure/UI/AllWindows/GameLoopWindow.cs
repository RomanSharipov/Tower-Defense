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
        [SerializeField] private TMP_Text _playerHealth;

        [Inject] private IAppStateService _appStateService;
        [Inject] private IGameLoopStatesService _gameLoopStatesService;
        [Inject] private IWavesService _wavesService;
        [Inject] private IPlayerHealthService _playerHealthService;
        [Inject] private BuildingTurretState _buildingTurretState;


        private void OnBuildButtonClicked(TurretId id)
        {
            _buildingTurretState.Setup(id);
            _gameLoopStatesService.Enter<BuildingTurretState>();
        }
        [ContextMenu("CurrentHealth()")]
        private void CurrentHealth()
        {
            Debug.Log($"CurrentHealth = {_playerHealthService.CurrentHealth}");
        }
        
        public override void Initialize()
        {
            _playerHealthService.HealthChanged += UpdatePlayerHealth;

            _goToMenuButton.OnClickAsObservable().Subscribe(_ =>
            {
                _appStateService.Enter<MenuState>();
            }).AddTo(this);
            _nextWave.OnClickAsObservable().Subscribe(_ =>
            {
                _wavesService.ProceedToNextWave();
                UpdateCurrentWavesText();
            }).AddTo(this);

            _pauseButton.OnClickAsObservable().Subscribe(_ =>
            {
                _gameLoopStatesService.Enter<PauseState>();
            }).AddTo(this);
            
            foreach (BuildButton buildButton in _buildTurretButtons)
            {
                buildButton.Clicked += OnBuildButtonClicked;
            }
            UpdateCurrentWavesText();
            UpdatePlayerHealth();
        }

        private void UpdatePlayerHealth()
        {
            _playerHealth.text = $"{_playerHealthService.CurrentHealth}/{_playerHealthService.MaxHealth}";
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
            _playerHealthService.HealthChanged -= UpdatePlayerHealth;
        }
    }
}
