using System.Collections.Generic;
using Assets.Scripts.CoreGamePlay;
using CodeBase.Configs;
using CodeBase.Infrastructure.UI.Services;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services
{
    public class AddressablesAssetReferencesService : IAddressablesAssetReferencesService
    {
        private readonly AddressablesAssetReferencesData _mainStaticData;
        public AddressablesAssetReferencesService(AddressablesAssetReferencesData mainStaticData)
        {
            _mainStaticData = mainStaticData;
        }
        public AssetReference RootCanvas =>
            _mainStaticData.WindowsData.rootCanvas;

        public IReadOnlyDictionary<WindowId, AssetReference> Windows => 
            _mainStaticData.WindowsData.WindowAssetReference.ToDictionary<WindowId, AssetReference>();


        public IReadOnlyDictionary<string, AssetReference> SceneAssetReferences => 
            _mainStaticData.SceneReferences.ToDictionary<AssetReference>();

        public IReadOnlyList<AssetReference> LevelReferences =>
            _mainStaticData.LevelReferences;
        
        public IReadOnlyDictionary<TileId, AssetReference> Tiles =>
            _mainStaticData.Tiles.ToDictionary<TileId, AssetReference>();

        public IReadOnlyDictionary<EnemyType, AssetReference> Enemies =>
            _mainStaticData.Enemies.ToDictionary<EnemyType, AssetReference>();

        public IReadOnlyDictionary<TurretId, AssetReference> Turrets =>
            _mainStaticData.Turrets.ToDictionary<TurretId, AssetReference>();
    }
}   