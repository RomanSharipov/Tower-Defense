using System.Collections.Generic;
using Assets.Scripts.CoreGamePlay;

namespace CodeBase.Infrastructure.Services
{
    public interface ICacherOfPath
    {
        public void SetSpawnersOnCurrentLevel(EnemySpawner[] enemySpawners);
        public bool TrySetPath();
        public IReadOnlyDictionary<EnemySpawner, List<TileData>> Paths { get; }
        public bool PathsIsExist { get; }
    }
}