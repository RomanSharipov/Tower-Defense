using Assets.Scripts.Infrastructure;
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
            builder.RegisterEntryPoint<EntryPoint>();
        }
    }    
}
