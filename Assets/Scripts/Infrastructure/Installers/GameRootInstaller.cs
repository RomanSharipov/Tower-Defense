using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "GameRootInstaller",
    menuName = "Scriptable Installers/GameRootInstaller")]
    public class GameRootInstaller : AScriptableInstaller
    {
        private GameRoot _gameRoot;

        public override void Install(IContainerBuilder builder)
        {
            _gameRoot = new GameRoot();

            builder.RegisterInstance(_gameRoot)
                .AsSelf();

            builder.RegisterBuildCallback(resolver =>
            {
                _gameRoot.Construct(resolver);
                _gameRoot.CreateStatemachine();
                _gameRoot.StartGame();
            });

        }
    }
}
