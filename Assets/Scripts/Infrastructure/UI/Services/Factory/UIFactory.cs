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
        private readonly IObjectResolver _objectResolver;
        private readonly IReadOnlyDictionary<Type, AssetReference> _windowsData;
        private readonly AssetReference _rootCanvasPrefab;

        private Transform _rootCanvas;

        [Inject]
        public UIFactory(IAssetProvider assetProvider, IObjectResolver objectResolver, IReadOnlyDictionary<Type, AssetReference> windowsData, AssetReference rootCanvasPrefab)
        {
            _assetProvider = assetProvider;
            _objectResolver = objectResolver;
            _windowsData = windowsData;
            _rootCanvasPrefab = rootCanvasPrefab;
        }

        public async UniTask CreateRootCanvas()
        {
            GameObject rootCanvasPrefab = await _assetProvider.Load<GameObject>(_rootCanvasPrefab);
            GameObject rootCanvasGameobject = GameObject.Instantiate(rootCanvasPrefab);
            _rootCanvas = rootCanvasGameobject.transform;
        }

        public async UniTask<TWindow> CreateWindow<TWindow>() where TWindow : WindowBase
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(_windowsData[typeof(TWindow)]);
            GameObject newGameObject = GameObject.Instantiate(prefab, _rootCanvas);
            _objectResolver.InjectGameObject(newGameObject);
            TWindow windowComponent = newGameObject.GetComponent<TWindow>();
            return windowComponent;
        }
    }
    

}
