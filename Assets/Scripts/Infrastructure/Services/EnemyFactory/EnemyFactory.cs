using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using UnityEngine.AddressableAssets;
using Assets.Scripts.CoreGamePlay;
using VContainer.Unity;

namespace Assets.Scripts.Infrastructure.Services
{

    public class EnemyFactory : IEnemyFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IObjectResolver _objectResolver;
        private readonly IReadOnlyDictionary<EnemyType, AssetReference> _assetReferenceData;

        [Inject]
        public EnemyFactory(IAssetProvider assetProvider, IObjectResolver objectResolver, IAddressablesAssetReferencesService assetReferenceData)
        {
            _assetProvider = assetProvider;
            _objectResolver = objectResolver;
            _assetReferenceData = assetReferenceData.Enemies;
        }
        
        public async UniTask<EnemyBase> CreateEnemy(EnemyType enemyType) 
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(_assetReferenceData[enemyType]);
            GameObject newGameObject = GameObject.Instantiate(prefab);
            _objectResolver.InjectGameObject(newGameObject);
            EnemyBase enemyComponent = newGameObject.GetComponent<EnemyBase>();
            return enemyComponent;
        }
    }

    [Serializable]
    public class EnemyAssetReference
    {
        public EnemyType enemyType;
        public AssetReference AssetReference;
    }
}
