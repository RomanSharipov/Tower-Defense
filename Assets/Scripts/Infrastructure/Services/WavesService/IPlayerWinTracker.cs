using System.Collections.Generic;
using Assets.Scripts.CoreGamePlay;

namespace CodeBase.Infrastructure.Services
{
    public interface IPlayerWinTracker
    {
        public void StartTracking(List<EnemyBase> enemies);
    }
}