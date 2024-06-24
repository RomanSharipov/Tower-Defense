using UnityEngine;
using VContainer;

public abstract class AScriptableInstaller : ScriptableObject
{
    public abstract void Install(IContainerBuilder builder);
}
