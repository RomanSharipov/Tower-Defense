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
        private readonly IReadOnlyDictionary<WindowId, AssetReference> _assetReferenceData;
        private readonly IObjectResolver _objectResolver;
        private readonly AssetReference _rootCanvasPrefab;

        private Transform _rootCanvas;

        [Inject]
        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticDataService, IObjectResolver objectResolver)
        {
            _assetProvider = assetProvider;
            _assetReferenceData = staticDataService.Windows;
            _objectResolver = objectResolver;
            _rootCanvasPrefab = staticDataService.RootCanvas;
        }

        public async UniTask CreateRootCanvas()
        {
            GameObject rootCanvasPrefab = await _assetProvider.Load<GameObject>(_rootCanvasPrefab);
            GameObject rootCanvasGameobject = GameObject.Instantiate(rootCanvasPrefab);
            _rootCanvas = rootCanvasGameobject.transform;
        }

        public async UniTask<T> CreateWindow<T>(WindowId windowType) where T : Component
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(_assetReferenceData[windowType]);
            GameObject newGameObject = GameObject.Instantiate(prefab, _rootCanvas);
            T windowComponent = newGameObject.GetComponent<T>();
            _objectResolver.Inject(windowComponent);
            return windowComponent;
        }
    }
    
    [Serializable]
    public class WindowAssetReference
    {
        public WindowId WindowType;
        public AssetReference assetReference;
    }

    [Serializable]
    public class WindowsData
    {
        public WindowAssetReference[] WindowAssetReference;
        public AssetReference rootCanvas;
    }
}
