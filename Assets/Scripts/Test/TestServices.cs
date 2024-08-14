using Assets.Scripts.CoreGamePlay;
using Assets.Scripts.CoreGamePlay.Enemy;
using Assets.Scripts.Infrastructure.Services;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.UI.Services;
using UnityEngine;
using VContainer;

public class TestServices : MonoBehaviour
{
    [Inject] private ISceneLoader sceneLoader;
    [Inject] private IObjectResolver IObjectResolver;
    [Inject] private ITurretFactory _tileFactory;
    [Inject] private IEnemyFactory _enemyFactory;

    
    [ContextMenu("CreateTile_Empty")]
    public void CreateTile_Empty()
    {
        _tileFactory.CreateTile(TileId.Empty);
    }

    [ContextMenu("CreateTile_Obstacle")]
    public void CreateTile_Obstacle()
    {
        _tileFactory.CreateTile(TileId.Obstacle);
    }

    [ContextMenu("CreateEnemy")]
    public void CreateEnemy()
    {
        _enemyFactory.CreateEnemy<Tank>(EnemyType.Tank);
    }
}
