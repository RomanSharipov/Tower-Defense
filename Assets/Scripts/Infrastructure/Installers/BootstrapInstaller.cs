using System.Collections;
using System.Collections.Generic;
using Scripts.Infrastructure.Installers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scripts.Infrastructure
{
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
}