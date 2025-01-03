using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.UI.Services
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IReadOnlyDictionary<Type, AssetReference> _assetReferenceData;
        private readonly IObjectResolver _objectResolver;
        private readonly AssetReference _rootCanvasPrefab;

        private Transform _rootCanvas;

        [Inject]
        public UIFactory(IAssetProvider assetProvider, IAddressablesAssetReferencesService _addressablesAssetReferencesService, IObjectResolver objectResolver)
        {
            _assetProvider = assetProvider;
            _assetReferenceData = _addressablesAssetReferencesService.Windows;
            _objectResolver = objectResolver;
            _rootCanvasPrefab = _addressablesAssetReferencesService.RootCanvas;
        }

        public async UniTask CreateRootCanvas()
        {
            GameObject rootCanvasPrefab = await _assetProvider.Load<GameObject>(_rootCanvasPrefab);
            GameObject rootCanvasGameobject = GameObject.Instantiate(rootCanvasPrefab);
            _rootCanvas = rootCanvasGameobject.transform;
        }

        public async UniTask<TWindow> CreateWindow<TWindow>() where TWindow : WindowBase
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(_assetReferenceData[typeof(TWindow)]);
            GameObject newGameObject = GameObject.Instantiate(prefab, _rootCanvas);
            _objectResolver.InjectGameObject(newGameObject);
            TWindow windowComponent = newGameObject.GetComponent<TWindow>();
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
        public AssetReference rootCanvas;
        public WindowAssetReference[] WindowAssetReference;
    }
}
