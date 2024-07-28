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
        private readonly IReadOnlyDictionary<WindowType, AssetReference> _assetReferenceData;

        private Transform _rootCanvas;

        [Inject]
        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _assetReferenceData = staticDataService.Windows;
        }

        public UniTask CreateShop()
        {
            throw new NotImplementedException();
        }

        public async UniTask CreateRootCanvas()
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(_assetReferenceData[WindowType.RootCanvas]);
            _rootCanvas = GameObject.Instantiate(prefab).transform;
        }
    }

    public enum WindowType
    {
        None,
        RootCanvas,
        Shop
    }

    [Serializable]
    public class WindowAssetReference
    {
        public WindowType WindowType;
        public AssetReference assetReference;
    }
}
