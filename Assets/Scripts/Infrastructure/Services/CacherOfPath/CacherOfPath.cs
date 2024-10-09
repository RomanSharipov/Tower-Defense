using System.Collections.Generic;
using Assets.Scripts.CoreGamePlay;
using BezierSolution;
using Tarodev_Pathfinding._Scripts;

namespace CodeBase.Infrastructure.Services
{
    public class CacherOfPath : ICacherOfPath
    {
        private EnemySpawner[] _spawnersOnCurrentLevel;
        private Dictionary<EnemySpawner, List<TileData>> _tilesPaths = new Dictionary<EnemySpawner, List<TileData>>();
        private Dictionary<EnemySpawner, BezierSpline> _pathFly = new Dictionary<EnemySpawner, BezierSpline>();
        private bool _pathsIsExist;

        public IReadOnlyDictionary<EnemySpawner, List<TileData>> TilesPaths => _tilesPaths;
        public IReadOnlyDictionary<EnemySpawner, BezierSpline> PathFly => _pathFly;
        public bool PathsIsExist => _pathsIsExist;

        public void RegisterFlyPath(EnemySpawner enemySpawner, BezierSpline bezierPoints)
        {
            _pathFly.Add(enemySpawner, bezierPoints);
        }

        public void SetSpawnersOnCurrentLevel(EnemySpawner[] enemySpawners)
        {
            _spawnersOnCurrentLevel = enemySpawners;
        }

        public bool TryBuildPath()
        {
            foreach (EnemySpawner enemySpawner in _spawnersOnCurrentLevel)
            {
                List<TileData> newPath = Pathfinding.FindPath(enemySpawner.StartTile.NodeBase, enemySpawner.TargetTile.NodeBase);
                if (newPath == null)
                {
                    _pathsIsExist = false;
                    _tilesPaths.Clear();
                    return false;
                }
                else
                {
                    newPath.Add(enemySpawner.StartTile.NodeBase);
                    newPath.Reverse();
                    _tilesPaths[enemySpawner] = newPath;
                }
            }
            _pathsIsExist = true;
            return true;
        }
    }
}