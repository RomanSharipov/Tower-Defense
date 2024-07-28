using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using VContainer;

namespace CodeBase.Infrastructure.Services
{
    public class SceneLoader : ISceneLoader
    {
        private readonly Dictionary<SceneName, AssetReference> _sceneReferences;
        private readonly IAssetProvider _assetProvider;

        [Inject]
        public SceneLoader(Dictionary<SceneName, AssetReference> sceneReferences, IAssetProvider assetProvider)
        {
            _sceneReferences = sceneReferences;
            _assetProvider = assetProvider;
        }

        public async UniTask<Scene> Load(SceneName name, Action onLoaded = null)
        {
            if (_sceneReferences.TryGetValue(name, out AssetReference sceneReference))
            {
                Scene result = await _assetProvider.LoadScene(sceneReference);
                onLoaded?.Invoke();
                return result;
            }
            else
            {
                Debug.LogError($"Scene {name} not found in _sceneReferences Dictionary.");
                return default;
            }
        }

        public void Unload(SceneName name)
        {
            if (_sceneReferences.TryGetValue(name, out AssetReference handle))
            {
                _assetProvider.ReleaseScene(handle);
                return;
            }
            else
            {
                Debug.LogError($"Scene {name} is not loaded");
                return;
            }
        }
    }

    public enum SceneName
    {
        None,
        Menu
    }

    [Serializable]
    public class SceneReference
    {
        public SceneName SceneName;
        public AssetReference Reference;
    }
}

