using System;
using VContainer;

namespace CodeBase.Infrastructure.Services
{
    public class PlayerWinTracker : IPlayerWinTracker
    {
        public event Action PlayerWon;

        private IWavesService _wavesService;
        private IAllEnemyStorage _allEnemyStorage;

        [Inject]
        public PlayerWinTracker(IWavesService wavesService, IAllEnemyStorage allEnemyStorage)
        {
            _wavesService = wavesService;
            _allEnemyStorage = allEnemyStorage;
        }
        public void CheckWin()
        {
            if (!_wavesService.AllWavesIsOver)
                return;

            if (_allEnemyStorage.Count > 0)
                return;

            PlayerWon?.Invoke();
        }
    }
}
