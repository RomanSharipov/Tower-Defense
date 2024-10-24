using System;
using System.Collections.Generic;
using Assets.Scripts.CoreGamePlay;
using CodeBase.Configs;
using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
    public class PlayerWinTracker : IPlayerWinTracker
    {
        public List<EnemyBase> _enemies;

        public void StartTracking(List<EnemyBase> enemies)
        {
            _enemies = enemies;

            foreach (EnemyBase enemy in _enemies)
            {
                enemy.Died += Track;
                enemy.GoalIsReached += Track;
            }
        }

        public void EndTracking()
        {
            foreach (EnemyBase enemy in _enemies)
            {
                enemy.Died -= Track;
                enemy.GoalIsReached -= Track;
            }
            _enemies = null;
        }

        private void Track(EnemyBase enemy)
        {
            Debug.Log($"_enemies{enemy.gameObject.name} = {_enemies.Count}");
        }
    }
}
