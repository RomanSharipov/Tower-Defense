using CodeBase.Infrastructure.UI.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace CodeBase.Infrastructure.Installers
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
}
