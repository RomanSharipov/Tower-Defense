using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.CoreGamePlay
{
    public class TurretFactory : ITurretFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IReadOnlyDictionary<TileId, AssetReference> _assetReferenceTileData;
        private readonly IReadOnlyDictionary<TurretId, AssetReference> _assetReferenceTurretsData;
        private readonly IObjectResolver _objectResolver;
        private Transform _turrentParrent;

        [Inject]
        public TurretFactory(IAssetProvider assetProvider, IAddressablesAssetReferencesService staticDataService, IObjectResolver objectResolver)
        {
            _assetProvider = assetProvider;
            _assetReferenceTileData = staticDataService.Tiles;
            _assetReferenceTurretsData = staticDataService.Turrets;
            _objectResolver = objectResolver;
        }

        public async UniTask<TileView> CreateTile(TileId TileId)
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(_assetReferenceTileData[TileId]);
            GameObject newGameObject = UnityEngine.Object.Instantiate(prefab);
            TileView tile = newGameObject.GetComponent<TileView>();
            return tile;
        }

        public async UniTask<TurretBase> CreateTurret(TurretId turretType) 
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(_assetReferenceTurretsData[turretType]);
            GameObject newGameObject = GameObject.Instantiate(prefab, _turrentParrent);
            _objectResolver.InjectGameObject(newGameObject);
            TurretBase turret = newGameObject.GetComponent<TurretBase>();
            return turret;
        }

        public void SetParrentTurret(Transform turrentParrent)
        {
            _turrentParrent = turrentParrent;
        }
    }

    [Serializable]
    public class TileIdAssetReference
    {
        public TileId WindowType;
        public AssetReference assetReference;
    }

    [Serializable]
    public class TurretAssetReference
    {
        public TurretId turretType;
        public AssetReference AssetReference;
    }
}


