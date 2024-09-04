using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using UnityEngine.AddressableAssets;
using Assets.Scripts.CoreGamePlay;

namespace Assets.Scripts.Infrastructure.Services
{

    public class EnemyFactory : IEnemyFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IObjectResolver _objectResolver;
        private readonly IReadOnlyDictionary<EnemyType, AssetReference> _assetReferenceData;

        [Inject]
        public EnemyFactory(IAssetProvider assetProvider, IObjectResolver objectResolver, IAddressablesAssetReferencesService staticDataService)
        {
            _assetProvider = assetProvider;
            _objectResolver = objectResolver;
            _assetReferenceData = staticDataService.Enemies;
        }

        //public async UniTask<T> CreateEnemy<T>(EnemyType windowType) where T : EnemyBase
        //{
        //    GameObject prefab = await _assetProvider.Load<GameObject>(_assetReferenceData[windowType]);
        //    GameObject newGameObject = GameObject.Instantiate(prefab);
        //    T windowComponent = newGameObject.GetComponent<T>();
        //    _objectResolver.Inject(windowComponent);
        //    return windowComponent;
        //}

        public async UniTask<T> CreateEnemy<T>(EnemyType enemyType) where T : EnemyBase
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(_assetReferenceData[enemyType]);
            GameObject newGameObject = GameObject.Instantiate(prefab);
            T windowComponent = newGameObject.GetComponent<T>();
            _objectResolver.Inject(windowComponent);
            return windowComponent;
        }
    }

    [Serializable]
    public class EnemyAssetReference
    {
        public EnemyType enemyType;
        public AssetReference AssetReference;
    }
}
