using System.Collections.Generic;
using Assets.Scripts.CoreGamePlay;
using BezierSolution;

namespace CodeBase.Infrastructure.Services
{
    public interface ICacherOfPath
    {
        public void SetSpawnersOnCurrentLevel(EnemySpawner[] enemySpawners);
        public bool TryBuildPath();
        public IReadOnlyDictionary<EnemySpawner, List<TileData>> TilesPaths { get; }
        public bool PathsIsExist { get; }
        public IReadOnlyDictionary<EnemySpawner, BezierSpline> PathFly { get; }
        public void RegisterFlyPath(EnemySpawner enemySpawner,BezierSpline bezierPoints);
    }
}