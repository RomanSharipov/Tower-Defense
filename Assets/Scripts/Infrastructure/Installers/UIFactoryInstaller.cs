using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Infrastructure.UI;
using Assets.Scripts.Infrastructure.UI.Services;
using Scripts.Infrastructure.Installers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Assets.Scripts.Infrastructure.Installers
{
    [CreateAssetMenu(
    fileName = "UIFactoryInstaller",
    menuName = "Scriptable Installers/UIFactoryInstaller"
)]
    
    public class UIFactoryInstaller : AScriptableInstaller
    {
        [SerializeField] private UIAssetReferenceData[] _uIAssetReferences;

        public override void Install(IContainerBuilder builder)
        {
            builder.Register<UIFactory>(Lifetime.Singleton)
                .WithParameter("assetReferenceData", _uIAssetReferences.ToDictionary<UIAsset, AssetReference>())
                .As<IUIFactory>();
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
