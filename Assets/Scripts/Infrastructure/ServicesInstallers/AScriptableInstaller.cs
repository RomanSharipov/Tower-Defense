using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure.Installers
{
    public abstract class AScriptableInstaller : ScriptableObject
    {
        public abstract void Install(IContainerBuilder builder);
    }
}