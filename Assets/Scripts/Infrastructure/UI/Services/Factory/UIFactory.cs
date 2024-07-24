using System;
using System.Collections.Generic;
using Assets.Scripts.Infrastructure.Installers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Assets.Scripts.Infrastructure.UI.Services
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
}
