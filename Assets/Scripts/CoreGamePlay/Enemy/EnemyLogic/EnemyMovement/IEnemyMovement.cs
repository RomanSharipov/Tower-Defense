using System;

namespace Assets.Scripts.CoreGamePlay
{
    public interface IEnemyMovement
    {
        public void Init(float speed, EnemySpawner enemySpawner);
        public void StartMovement();
        public void StopMovement();
        public void SlowDownMovement(int percent,float duration);
        public event Action GoalIsReached;
    }
}