using CodeBase.Configs;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "StaticDataServiceInstaller",
    menuName = "Scriptable Installers/StaticDataServiceInstaller")]
    
    public class StaticDataServiceInstaller : AScriptableInstaller
    {
        [SerializeField] private AddressablesAssetReferencesData _mainStaticData;

        public override void Install(IContainerBuilder builder)
        {
            builder.Register<AddressablesAssetReferencesService>(Lifetime.Singleton)
                .WithParameter("mainStaticData", _mainStaticData)
                .As<IAddressablesAssetReferencesService>();
        }
    }    
}
