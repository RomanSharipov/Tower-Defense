using Assets.Scripts.Infrastructure;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "EntryPointInstaller",
    menuName = "Scriptable Installers/EntryPointInstaller")]
    
    public class EntryPointInstaller : AScriptableInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            GameStatemachine mainGameStatemachine = new GameStatemachine();
            GameStatemachine gameLoopStatemachine = new GameStatemachine();

            builder.Register<GameLoopStatesService>(Lifetime.Singleton)
                .WithParameter("gameLoopStatemachine", gameLoopStatemachine)
                .As<IGameLoopStatesService>();

            builder.Register<AppStateService>(Lifetime.Singleton)
                .WithParameter("mainGameStatemachine", mainGameStatemachine)
                .As<IAppStateService>();

            RegisterStates(builder);

            builder.RegisterBuildCallback(resolver =>
            {
                mainGameStatemachine.AddState(typeof(BootstrapState), resolver.Resolve<BootstrapState>());
                mainGameStatemachine.AddState(typeof(MenuState), resolver.Resolve<MenuState>());
                mainGameStatemachine.AddState(typeof(GameLoopState), resolver.Resolve<GameLoopState>());

                gameLoopStatemachine.AddState(typeof(PlayingIdleState), resolver.Resolve<PlayingIdleState>());
                gameLoopStatemachine.AddState(typeof(BuildingTurretState), resolver.Resolve<BuildingTurretState>());
                gameLoopStatemachine.AddState(typeof(PauseState), resolver.Resolve<PauseState>());
                gameLoopStatemachine.AddState(typeof(TransitToNextLevelState), resolver.Resolve<TransitToNextLevelState>());
                gameLoopStatemachine.AddState(typeof(EmptyState), resolver.Resolve<EmptyState>());
                gameLoopStatemachine.AddState(typeof(PlayerWinState), resolver.Resolve<PlayerWinState>());
                gameLoopStatemachine.AddState(typeof(PlayerLoseSate), resolver.Resolve<PlayerLoseSate>());
                gameLoopStatemachine.AddState(typeof(RestartState), resolver.Resolve<RestartState>());
            });
            
            builder.RegisterEntryPoint<EntryPoint>();
        }

        private void RegisterStates(IContainerBuilder builder)
        {
            builder.Register<BootstrapState>(Lifetime.Singleton)
                .AsSelf();
            builder.Register<MenuState>(Lifetime.Singleton)
                .AsSelf();
            builder.Register<GameLoopState>(Lifetime.Singleton)
                .AsSelf();
            builder.Register<BuildingTurretState>(Lifetime.Singleton)
                .AsSelf();
            builder.Register<PlayingIdleState>(Lifetime.Singleton)
                .AsSelf();
            builder.Register<PauseState>(Lifetime.Singleton)
                .AsSelf();
            builder.Register<TransitToNextLevelState>(Lifetime.Singleton)
                .AsSelf();
            builder.Register<EmptyState>(Lifetime.Singleton)
                .AsSelf();
            builder.Register<PlayerWinState>(Lifetime.Singleton)
                .AsSelf();
            builder.Register<PlayerLoseSate>(Lifetime.Singleton)
                .AsSelf();
            builder.Register<RestartState>(Lifetime.Singleton)
                .AsSelf();
        }
    }    
}
