using System;
using System.Collections.Generic;
using Assets.Scripts.CoreGamePlay;
using CodeBase.Configs;
using CodeBase.Infrastructure.UI.Services;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services
{
    public class AddressablesAssetReferencesService : IAddressablesAssetReferencesService
    {
        private readonly AddressablesAssetReferencesData _assetReferencesData;
        public AddressablesAssetReferencesService(AddressablesAssetReferencesData assetReferencesData)
        {
            _assetReferencesData = assetReferencesData;
        }
        public AssetReference RootCanvas =>
            _assetReferencesData.WindowsData.rootCanvas;

        public IReadOnlyDictionary<Type, AssetReference> Windows => 
            _assetReferencesData.WindowsData.WindowAssetReference.ToDictionary<Type, AssetReference>();


        public IReadOnlyDictionary<string, AssetReference> SceneAssetReferences => 
            _assetReferencesData.SceneReferences.ToDictionary<AssetReference>();

        public IReadOnlyList<AssetReference> LevelReferences =>
            _assetReferencesData.LevelReferences;
        
        public IReadOnlyDictionary<TileId, AssetReference> Tiles =>
            _assetReferencesData.Tiles.ToDictionary<TileId, AssetReference>();

        public IReadOnlyDictionary<EnemyType, AssetReference> Enemies =>
            _assetReferencesData.Enemies.ToDictionary<EnemyType, AssetReference>();

        public IReadOnlyDictionary<TurretId, AssetReference> Turrets =>
            _assetReferencesData.Turrets.ToDictionary<TurretId, AssetReference>();
    }
}   