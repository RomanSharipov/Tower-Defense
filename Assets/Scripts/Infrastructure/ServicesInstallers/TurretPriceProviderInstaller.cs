using CodeBase.Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "TurretPriceProviderInstaller",
    menuName = "Scriptable Installers/TurretPriceProviderInstaller")]

    public class TurretPriceProviderInstaller : AScriptableInstaller
    {
        [SerializeField] private TurretsPriceConfig _turretsPriceConfig;

        public override void Install(IContainerBuilder builder)
        {
            builder.Register<TurretPriceProvider>(Lifetime.Singleton)
                .WithParameter("turretsPriceConfig", _turretsPriceConfig)
                .As<ITurretPriceProvider>();
        }
    }
}
