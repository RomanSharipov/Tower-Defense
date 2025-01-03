using System.Collections.Generic;
using Assets.Scripts.CoreGamePlay;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services
{
    public interface IAddressablesAssetReferencesService
    {
        public IReadOnlyDictionary<string, AssetReference> SceneAssetReferences { get; }
        public IReadOnlyDictionary<TileId, AssetReference> Tiles { get; }
        public IReadOnlyDictionary<EnemyType, AssetReference> Enemies { get; }
        public IReadOnlyDictionary<TurretId, AssetReference> Turrets { get; }
        public IReadOnlyList<AssetReference> LevelReferences { get; }
    }
}