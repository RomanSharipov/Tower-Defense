using System;
using System.Collections.Generic;
using Assets.Scripts.CoreGamePlay;

namespace CodeBase.Infrastructure.Services
{
    public interface IPlayerWinTracker
    {
        public void EndTracking();
        public void StartTracking(List<EnemyBase> enemies);
        public event Action PlayerWon;
    }
}