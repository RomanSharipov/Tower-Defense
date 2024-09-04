using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.UI.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using VContainer;

namespace CodeBase.Infrastructure.Services
{
    public class SceneLoader : ISceneLoader
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IReadOnlyDictionary<string, AssetReference> _sceneReferences;

        [Inject]
        public SceneLoader(IAddressablesAssetReferencesService staticDataService, IAssetProvider assetProvider)
        {
            _sceneReferences = staticDataService.SceneAssetReferences;
            _assetProvider = assetProvider;
        }

        public async UniTask<Scene> Load(string name, Action onLoaded = null)
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

        public void Unload(string name)
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
    [Serializable]
    public class SceneReference
    {
        public string SceneName;
        public AssetReference Reference;
    }
}

