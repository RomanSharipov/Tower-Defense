using System;
using System.Collections.Generic;
using Assets.Scripts.CoreGamePlay;

namespace CodeBase.Infrastructure.Services
{
    public class PlayerWinTracker : IPlayerWinTracker
    {
        public List<EnemyBase> _enemies;
        public event Action PlayerWon;

        public bool TrackingEnabled { get; private set; }

        public void StartTracking(List<EnemyBase> enemies)
        {
            TrackingEnabled = true;
            _enemies = enemies;

            foreach (EnemyBase enemy in _enemies)
            {
                enemy.Died += Track;
                enemy.GoalIsReached += Track;
            }
        }

        public void EndTracking()
        {
            TrackingEnabled = false;
            foreach (EnemyBase enemy in _enemies)
            {
                enemy.Died -= Track;
                enemy.GoalIsReached -= Track;
            }
            _enemies = null;
        }

        private void Track(EnemyBase enemy)
        {
            if (!TrackingEnabled)
                return;

            if (_enemies.Count == 0)
            {
                EndTracking();
                PlayerWon?.Invoke();
            }
        }
    }
}
