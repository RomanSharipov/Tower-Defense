using Scripts.Infrastructure.Installers;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using VContainer;

namespace Assets.Scripts.Infrastructure
{
    [CreateAssetMenu(
    fileName = "GameRootInstaller",
    menuName = "Scriptable Installers/GameRootInstaller"
)]
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
                _gameRoot.Start();
            });

        }
    }
}
