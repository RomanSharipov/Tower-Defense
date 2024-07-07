using UnityEngine;
using VContainer;

namespace Scripts.Infrastructure.Installers
{
    public abstract class AScriptableInstaller : ScriptableObject
    {
        public abstract void Install(IContainerBuilder builder);
    }
}