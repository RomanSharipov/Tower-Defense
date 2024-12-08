using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.UI.Services;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "TurretsStatsDataInstaller",
    menuName = "Scriptable Installers/TurretsStatsDataInstaller")]
    
    public class TurretsStatsDataInstaller : AScriptableInstaller
    {
        [SerializeField] private TurretsLevelsConfig _turretsLevelsConfig;

        public override void Install(IContainerBuilder builder)
        {
            builder.Register<TurretsStatsData>(Lifetime.Singleton)
                .WithParameter("turretsLevelsConfig", _turretsLevelsConfig)
                .As<ITurretsStatsData>();
        }
    }    
}
