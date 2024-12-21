using Assets.Scripts.CoreGamePlay;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.UI.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

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
            builder.Register<CacherOfPath>(Lifetime.Singleton)
                .As<ICacherOfPath>();
            builder.Register<TilesStorage>(Lifetime.Singleton)
                .As<ITilesStorage>();
            builder.Register<WavesService>(Lifetime.Singleton)
                .As<IWavesService>();
            builder.Register<GameStatusService>(Lifetime.Singleton)
                .As<IGameStatusService>();
            builder.Register<AllEnemyStorage>(Lifetime.Singleton)
                .As<IAllEnemyStorage>();
            builder.Register<InputService>(Lifetime.Singleton)
                .As<IInputService>()
                .As<IInitializable>();

            builder.Register<ClickOnTurretTracker>(Lifetime.Singleton)
                .As<IClickOnTurretTracker>();
            builder.Register<TurretRemover>(Lifetime.Singleton)
                .As<ITurretRemover>();

            builder.Register<PlayerPrefsSaveService>(Lifetime.Singleton)
                .As<ISaveService>();
            builder.Register<TurretsStatsLevelIndexService>(Lifetime.Singleton)
                .As<ITurretsStatsLevelIndexService>()
                .As<IInitializable>();
        }
    }    
}
