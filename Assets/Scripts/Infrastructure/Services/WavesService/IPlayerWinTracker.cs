using System;
using System.Collections.Generic;
using Assets.Scripts.CoreGamePlay;

namespace CodeBase.Infrastructure.Services
{
    public interface IPlayerWinTracker
    {
        public void TrackWin();

        public event Action PlayerWon;
    }
}