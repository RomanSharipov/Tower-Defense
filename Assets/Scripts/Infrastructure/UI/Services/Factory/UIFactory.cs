using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace CodeBase.Infrastructure.UI.Services
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IReadOnlyDictionary<UIAsset, AssetReference> _assetReferenceData;

        private Transform _rootCanvas;

        [Inject]
        public UIFactory(IAssetProvider assetProvider, IReadOnlyDictionary<UIAsset, AssetReference> assetReferenceData)
        {
            _assetProvider = assetProvider;
            _assetReferenceData = assetReferenceData;
        }

        public UniTask CreateShop()
        {
            throw new NotImplementedException();
        }

        public async UniTask CreateRootCanvas()
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(_assetReferenceData[UIAsset.RootCanvas]);
            _rootCanvas = GameObject.Instantiate(prefab).transform;
        }
    }

    public enum UIAsset
    {
        None,
        RootCanvas,
        Shop
    }

    [Serializable]
    public class UIAssetReferenceData
    {
        public UIAsset UIType;
        public AssetReference assetReference;
    }
}
