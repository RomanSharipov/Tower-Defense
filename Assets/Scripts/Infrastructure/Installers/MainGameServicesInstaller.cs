using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure.Installers
{
    [CreateAssetMenu(
    fileName = "MainGameServicesInstaller",
    menuName = "Scriptable Installers/MainGameServicesInstaller"
)]
    public class MainGameServicesInstaller : AScriptableInstaller
    {
        public override void Install(IContainerBuilder builder)
        {

        }
    }
}
