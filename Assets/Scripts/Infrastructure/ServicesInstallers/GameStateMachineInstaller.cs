using CodeBase.Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "GameStateMachineInstaller",
    menuName = "Scriptable Installers/GameStateMachineInstaller")]
    
    public class GameStateMachineInstaller : AScriptableInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            GameStatemachine mainGameStatemachine = new GameStatemachine();
            GameStatemachine gameLoopStatemachine = new GameStatemachine();

            AppStateService gameStatusService = new AppStateService(mainGameStatemachine, gameLoopStatemachine);

            builder.RegisterInstance(gameStatusService)
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
            });

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
        }
    }    
}
