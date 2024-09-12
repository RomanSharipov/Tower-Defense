using System.Collections.Generic;
using CodeBase.Infrastructure.Installers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure
{
    public class LevelInstaller : LifetimeScope
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