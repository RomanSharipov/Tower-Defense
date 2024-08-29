using System.Collections.Generic;
using Assets.Scripts.CoreGamePlay;
using Tarodev_Pathfinding._Scripts;
using Unity.VisualScripting;

public class CacherOfPath : ICacherOfPath
{
    private EnemySpawner[] _spawnersOnCurrentLevel;
    private Dictionary<EnemySpawner, List<TileData>> _paths = new Dictionary<EnemySpawner, List<TileData>>();
    private bool _pathsIsExist;

    public IReadOnlyDictionary<EnemySpawner, List<TileData>> Paths => _paths;
    public bool PathsIsExist => _pathsIsExist;

    public void SetSpawnersOnCurrentLevel(EnemySpawner[] enemySpawners)
    {
        _spawnersOnCurrentLevel = enemySpawners;
    }

    public bool TrySetPath()
    {
        foreach (EnemySpawner enemySpawner in _spawnersOnCurrentLevel)
        {
            List<TileData> newPath = Pathfinding.FindPath(enemySpawner.StartTile.NodeBase, enemySpawner.TargetTile.NodeBase);
            if (newPath == null)
            {
                _pathsIsExist = false;
                _paths.Clear();
                return false;
            }
            else
            {
                newPath.Add(enemySpawner.StartTile.NodeBase);
                newPath.Reverse();
                _paths[enemySpawner] = newPath;
            }
        }
        _pathsIsExist = true;
        return true;
    }
}