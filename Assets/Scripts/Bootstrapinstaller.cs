using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class BootstrapInstaller : LifetimeScope
{
    [Header("Scriptable Installers")]
    [SerializeField]
    private List<AScriptableInstaller> _mainInstallers;

    protected override void Configure(IContainerBuilder builder)
    {
        foreach (AScriptableInstaller installer in _mainInstallers)
        {
            installer.Install(builder);
        }
    }
}
