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
            Debug.Log($"_assetReferenceData[UIAsset.RootCanvas] = {_assetReferenceData[UIAsset.RootCanvas]}");

            RootCanvas prefab = await _assetProvider.Load<RootCanvas>(_assetReferenceData[UIAsset.RootCanvas]);
            GameObject.Instantiate(prefab);
        }
    }
}
