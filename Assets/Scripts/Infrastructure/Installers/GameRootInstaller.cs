using Scripts.Infrastructure.Installers;
using UnityEngine;
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
            builder.RegisterBuildCallback(resolver =>
            {
                _gameRoot = new GameRoot(resolver);
                _gameRoot.Start();
            });
        }
    }
}
