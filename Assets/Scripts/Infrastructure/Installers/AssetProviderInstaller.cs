using CodeBase.Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure.Installers
{
    [CreateAssetMenu(
        fileName = "AssetProviderInstaller",
        menuName = "Scriptable Installers/AssetProviderInstaller"
    )]
    public class AssetProviderInstaller : AScriptableInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.Register<AddressableProvider>(Lifetime.Singleton)
                .As<IAssetProvider>();
        }
    }
}