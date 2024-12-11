using System.Collections.Generic;
using Assets.Scripts.CoreGamePlay;

namespace CodeBase.Infrastructure.Services
{
    public class AllEnemyStorage : IAllEnemyStorage
    {
        private List<EnemyBase> _allEnemies = new();

        public int Count => _allEnemies.Count;

        public void Add(EnemyBase enemyBase)
        {
            _allEnemies.Add(enemyBase);
        }

        public void Remove(EnemyBase enemyBase)
        {
            _allEnemies.Remove(enemyBase);
        }
    }
}
