using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.UI;
using CodeBase.Infrastructure.UI.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace CodeBase.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "UIFactoryInstaller",
        menuName = "Scriptable Installers/UIFactoryInstaller")]
    public class UIFactoryInstaller : AScriptableInstaller
    {
        [SerializeField] private AssetReference _rootCanvas;
        [SerializeField] private AssetReference _gameLoopWindow;
        [SerializeField] private AssetReference _loseWindow;
        [SerializeField] private AssetReference _mainMenu;
        [SerializeField] private AssetReference _turretContextMenu;
        [SerializeField] private AssetReference _winWindow;


        public override void Install(IContainerBuilder builder)
        {
            Dictionary<Type, AssetReference> windowsData = new()
            {
                [typeof(GameLoopWindow)] = _gameLoopWindow,
                [typeof(LoseWindow)] = _loseWindow,
                [typeof(MainMenu)] = _mainMenu,
                [typeof(TurretContextMenu)] = _turretContextMenu,
                [typeof(WinWindow)] = _winWindow,
            };


            builder.Register<UIFactory>(Lifetime.Singleton)
                .WithParameter("rootCanvasPrefab", _rootCanvas)
                .WithParameter("windowsData", windowsData)
                .As<IUIFactory>();
        }
    }
}