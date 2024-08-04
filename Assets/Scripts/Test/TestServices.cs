using Assets.Scripts.CoreGamePlay;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.UI.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

public class TestServices : MonoBehaviour
{
    [Inject] private ISceneLoader sceneLoader;
    [Inject] private IObjectResolver IObjectResolver;
    [Inject] private ITileFactory _tileFactory;

    
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
}
