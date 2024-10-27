using System;
using System.Collections.Generic;
using Assets.Scripts.CoreGamePlay;

namespace CodeBase.Infrastructure.Services
{
    public interface IPlayerWinTracker
    {
        public void CheckWin(int amountLivingEnemies);

        public event Action PlayerWon;
    }
}