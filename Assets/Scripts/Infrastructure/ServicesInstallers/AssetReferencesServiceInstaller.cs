using CodeBase.Configs;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "AssetReferencesServiceInstaller",
    menuName = "Scriptable Installers/AssetReferencesServiceInstaller")]
    
    public class AssetReferencesServiceInstaller : AScriptableInstaller
    {
        [SerializeField] private AddressablesAssetReferencesData _assetReferencesData;

        public override void Install(IContainerBuilder builder)
        {
            builder.Register<AddressablesAssetReferencesService>(Lifetime.Singleton)
                .WithParameter("assetReferencesData", _assetReferencesData)
                .As<IAddressablesAssetReferencesService>();
        }
    }    
}
