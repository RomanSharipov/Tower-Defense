using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Installers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure
{
    public class BootstrapInstaller : LifetimeScope
    {
        [Header("Scriptable Installers")]
        [SerializeField]
        private List<AScriptableInstaller> _mainInstallers;


        private IDisposable _globalParentOverride;
        protected override void Awake()
        {
            base.Awake();
            _globalParentOverride = EnqueueParent(this);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            _globalParentOverride?.Dispose();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            foreach (AScriptableInstaller installer in _mainInstallers)
            {
                installer.Install(builder);
            }
        }
    }
}