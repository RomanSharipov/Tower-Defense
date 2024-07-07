using Scripts.Infrastructure.Installers;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.Infrastructure
{
    [CreateAssetMenu(
    fileName = "MainGameServicesInstaller",
    menuName = "Scriptable Installers/MainGameServicesInstaller"
)]
    public class MainGameServicesInstaller : AScriptableInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.Register<SceneLoader>(Lifetime.Singleton)
                .As<ISceneLoader>();
        }
    }
}
