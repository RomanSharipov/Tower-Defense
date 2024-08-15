using Assets.Scripts.CoreGamePlay;
using Assets.Scripts.Infrastructure.Services;
using CodeBase.Infrastructure.UI.Services;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "AllServicesInstaller",
    menuName = "Scriptable Installers/AllServicesInstaller")]
    
    public class AllServicesInstaller : AScriptableInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.Register<WindowService>(Lifetime.Singleton)
                .As<IWindowService>();
            builder.Register<LevelService>(Lifetime.Singleton)
                .As<ILevelService>();
            builder.Register<TurretFactory>(Lifetime.Singleton)
                .As<ITurretFactory>();
            builder.Register<EnemyFactory>(Lifetime.Singleton)
                .As<IEnemyFactory>();
            builder.Register<BuildingService>(Lifetime.Singleton)
                .As<IBuildingService>();
        }
    }    
}
