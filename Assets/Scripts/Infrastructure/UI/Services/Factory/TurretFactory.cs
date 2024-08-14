using System;
using System.Collections.Generic;
using Assets.Scripts.CoreGamePlay;
using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace CodeBase.Infrastructure.UI.Services
{
    public class TurretFactory : ITurretFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IReadOnlyDictionary<TileId, AssetReference> _assetReferenceData;
        private readonly IObjectResolver _objectResolver;
        
        [Inject]
        public TurretFactory(IAssetProvider assetProvider, IStaticDataService staticDataService, IObjectResolver objectResolver)
        {
            _assetProvider = assetProvider;
            _assetReferenceData = staticDataService.Tiles;
            _objectResolver = objectResolver;
        }
        
        public async UniTask<TileView> CreateTile(TileId TileId) 
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(_assetReferenceData[TileId]);
            GameObject newGameObject = GameObject.Instantiate(prefab);
            TileView tile = newGameObject.GetComponent<TileView>();
            return tile;
        }

        //public async UniTask<T> CreateTile<T>(TileId windowType) where T : Component
        //{
        //    GameObject prefab = await _assetProvider.Load<GameObject>(_assetReferenceData[windowType]);
        //    GameObject newGameObject = GameObject.Instantiate(prefab, _rootCanvas);
        //    T windowComponent = newGameObject.GetComponent<T>();
        //    _objectResolver.Inject(windowComponent);
        //    return windowComponent;
        //}
    }
    
    [Serializable]
    public class TileIdAssetReference
    {
        public TileId WindowType;
        public AssetReference assetReference;
    }
}
