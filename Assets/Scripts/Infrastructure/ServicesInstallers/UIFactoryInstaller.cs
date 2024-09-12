using CodeBase.Infrastructure.UI.Services;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "UIFactoryInstaller",
    menuName = "Scriptable Installers/UIFactoryInstaller")]
    
    public class UIFactoryInstaller : AScriptableInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.Register<UIFactory>(Lifetime.Singleton)
                .As<IUIFactory>();
        }
    }    
}
