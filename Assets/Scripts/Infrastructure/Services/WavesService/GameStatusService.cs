using System;
using UniRx;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure.Services
{
    public class GameStatusService : IGameStatusService
    {
        private IWavesService _wavesService;
        private IAllEnemyStorage _allEnemyStorage;
        private ReactiveProperty<GameStatus> _gameStatus = new ();

        public IReactiveProperty<GameStatus> GameStatus => _gameStatus;
        
        [Inject]
        public GameStatusService(IWavesService wavesService, IAllEnemyStorage allEnemyStorage)
        {
            _wavesService = wavesService;
            _allEnemyStorage = allEnemyStorage;
        }
        public void TrackWin()
        {
            Debug.Log($"_allEnemyStorage.Count = {_allEnemyStorage.Count}");

            if (!_wavesService.AllWavesIsOver())
                return;
            
            if (_allEnemyStorage.Count > 0)
                return;
            SetStatus(Services.GameStatus.Win);
        }

        public void SetStatus(GameStatus gameStatus)
        {
            if (_gameStatus.Value != Services.GameStatus.None)
                return;

            _gameStatus.Value = gameStatus;
        }

        public void ResetStatus()
        {
            _gameStatus.Value = Services.GameStatus.None;
        }
    }

    public enum GameStatus { None,Win,Lose}
}
