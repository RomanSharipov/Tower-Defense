using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "GameRootInstaller",
    menuName = "Scriptable Installers/GameRootInstaller")]
    public class GameRootInstaller : AScriptableInstaller
    {
        private GameRoot _gameRoot;

        public override void Install(IContainerBuilder builder)
        {
            //_gameRoot = new GameRoot();



            //builder.RegisterBuildCallback(resolver =>
            //{
            //    resolver.Inject(_gameRoot);


            //    _gameRoot.Construct(resolver);
            //    _gameRoot.CreateStatemachine();
            //    _gameRoot.StartGame();
            //});
        }
    }
}
