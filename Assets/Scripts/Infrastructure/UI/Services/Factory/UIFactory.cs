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
        private readonly IObjectResolver _objectResolver;

        private Transform _rootCanvas;

        [Inject]
        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticDataService, IObjectResolver objectResolver)
        {
            _assetProvider = assetProvider;
            _assetReferenceData = staticDataService.Windows;
            _objectResolver = objectResolver;
        }

        public UniTask CreateShop()
        {
            throw new NotImplementedException();
        }

        public async UniTask CreateMainMenu()
        {
            GameObject mainMenuPrefab = await _assetProvider.Load<GameObject>(_assetReferenceData[WindowType.MainMenu]);
            GameObject newGameObject =  GameObject.Instantiate(mainMenuPrefab, _rootCanvas);
           MainMenu mainMenu = newGameObject.GetComponent<MainMenu>();
            _objectResolver.Inject(mainMenu);
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
        Shop,
        MainMenu
    }

    [Serializable]
    public class WindowAssetReference
    {
        public WindowType WindowType;
        public AssetReference assetReference;
    }
}
