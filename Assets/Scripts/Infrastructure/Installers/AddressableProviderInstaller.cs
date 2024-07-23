using UnityEngine;
using VContainer;

namespace Scripts.Infrastructure.Installers
{
    [CreateAssetMenu(
        fileName = "AddressableProviderInstaller",
        menuName = "Scriptable Installers/AddressableProviderInstaller"
    )]
    public class AddressableProviderInstaller : AScriptableInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.Register<AddressableProvider>(Lifetime.Singleton)
                .As<IAssetProvider>();
        }
    }
}