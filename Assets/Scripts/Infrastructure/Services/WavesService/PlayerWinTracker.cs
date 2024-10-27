using System;
using VContainer;

namespace CodeBase.Infrastructure.Services
{
    public class PlayerWinTracker : IPlayerWinTracker
    {
        public event Action PlayerWon;

        private IWavesService _wavesService;

        [Inject]
        public PlayerWinTracker(IWavesService wavesService)
        {
            _wavesService = wavesService;
        }
        public void CheckWin(int amountLivingEnemies)
        {
            if (!_wavesService.AllWavesIsOver)
                return;

            if (amountLivingEnemies > 0)
                return;

            PlayerWon?.Invoke();
        }
    }
}
