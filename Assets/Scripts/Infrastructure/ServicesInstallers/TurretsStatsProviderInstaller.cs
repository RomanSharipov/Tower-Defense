using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.UI.Services;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "TurretsStatsProviderInstaller",
    menuName = "Scriptable Installers/TurretsStatsProviderInstaller")]
    
    public class TurretsStatsProviderInstaller : AScriptableInstaller
    {
        [SerializeField] private TurretsLevelsConfig _turretsLevelsConfig;

        public override void Install(IContainerBuilder builder)
        {
            builder.Register<TurretsStatsProvider>(Lifetime.Singleton)
                .WithParameter("turretsLevelsConfig", _turretsLevelsConfig)
                .As<ITurretsStatsProvider>();
        }
    }    
}
