using System;
using System.Collections.Generic;
using Scripts.Infrastructure.Installers;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Assets.Scripts.Infrastructure
{
    [CreateAssetMenu(
    fileName = "SceneLoaderInstaller",
    menuName = "Scriptable Installers/SceneLoaderInstaller"
)]
    public class SceneLoaderInstaller : AScriptableInstaller
    {
        [SerializeField] private SceneReference[] _sceneReferences;

        public override void Install(IContainerBuilder builder)
        {
            Dictionary<SceneName, AssetReference> references = ToDictionary(_sceneReferences);

            builder.Register<SceneLoader>(Lifetime.Singleton)
                .WithParameter("sceneReferences", references)
                .As<ISceneLoader>();
        }

        private Dictionary<SceneName, AssetReference> ToDictionary(SceneReference[] sceneReferences)
        {
            Dictionary<SceneName, AssetReference> references = new Dictionary<SceneName, AssetReference>();

            foreach (SceneReference reference in sceneReferences)
            {
                references.Add(reference.SceneName, reference.Reference);
            }

            return references;
        }
    }

    [Serializable]
    public class SceneReference
    {
        public SceneName SceneName;
        public AssetReference Reference;
    }
}
