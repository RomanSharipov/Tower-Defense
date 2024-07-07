using Scripts.Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace Scripts.Infrastructure.Installers
{
    [CreateAssetMenu(
        fileName = "AllSoundsServiceInstaller",
        menuName = "Scriptable Installers/AllSoundsServiceInstaller"
    )]
    public class AllSoundsServiceInstaller : AScriptableInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.Register<AllSoundsService>(Lifetime.Singleton)
                .As<IAllSoundsService>();
        }
    }
}