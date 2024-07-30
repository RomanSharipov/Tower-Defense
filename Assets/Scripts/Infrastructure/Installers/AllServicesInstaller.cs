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
        }
    }    
}
