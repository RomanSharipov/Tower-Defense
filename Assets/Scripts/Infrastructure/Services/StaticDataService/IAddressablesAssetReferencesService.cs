using System.Collections.Generic;
using Assets.Scripts.CoreGamePlay;
using CodeBase.Infrastructure.UI.Services;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services
{
    public interface IAddressablesAssetReferencesService
    {
        public IReadOnlyDictionary<WindowId, AssetReference> Windows { get; }

        public IReadOnlyDictionary<string, AssetReference> SceneAssetReferences { get; }
        public AssetReference RootCanvas { get; }
        public IReadOnlyDictionary<TileId, AssetReference> Tiles { get; }
        public IReadOnlyDictionary<EnemyType, AssetReference> Enemies { get; }
        public IReadOnlyDictionary<TurretId, AssetReference> Turrets { get; }
        public IReadOnlyList<AssetReference> LevelReferences { get; }
    }
}