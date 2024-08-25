using System.Collections.Generic;
using Assets.Scripts.CoreGamePlay;

public interface ICacherOfPath
{
    public void SetSpawnersOnCurrentLevel(EnemySpawner[] enemySpawners);
    public bool TrySetPath();
    public IReadOnlyDictionary<EnemySpawner, TileData[]> Paths { get; }
    public bool PathsIsExist { get; }
}