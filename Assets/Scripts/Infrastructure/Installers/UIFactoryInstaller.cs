using System;
using System.Collections.Generic;
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
                .WithParameter("assetReferenceData", CovertToDictionary(_uIAssetReferences))
                .As<IUIFactory>();
        }

        private Dictionary<UIAsset, AssetReference> CovertToDictionary(IEnumerable<UIAssetReferenceData> assetReferenceList)
        {
            Dictionary<UIAsset, AssetReference> soundData = new Dictionary<UIAsset, AssetReference>();

            foreach (UIAssetReferenceData item in assetReferenceList)
            {
                soundData.Add(item.UIType, item.assetReference);
            }
            return soundData;
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
