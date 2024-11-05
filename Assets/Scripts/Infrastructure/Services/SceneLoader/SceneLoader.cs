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
        private readonly IAssetProvider _assetProvider;
        private readonly IReadOnlyDictionary<string, AssetReference> _sceneReferences;
        private readonly IReadOnlyList<AssetReference> _levels;
        
        [Inject]
        public SceneLoader(IAddressablesAssetReferencesService staticDataService, IAssetProvider assetProvider)
        {
            _sceneReferences = staticDataService.SceneAssetReferences;
            _levels = staticDataService.LevelReferences;
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

        public async UniTask<Scene> LoadLevel(int levelNumber, Action onLoaded = null)
        {
            int levelIndex = levelNumber - 1;

            CheckIndex(levelIndex);

            Scene result = await _assetProvider.LoadScene(_levels[levelIndex]);
            onLoaded?.Invoke();
            return result;
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

        public void UnloadLevel(int levelNumber)
        {
            int levelIndex = levelNumber - 1;

            CheckIndex(levelIndex);

            _assetProvider.ReleaseScene(_levels[levelIndex]);
        }

        private void CheckIndex(int levelIndex)
        {
            if (levelIndex < 0)
            {
                Debug.LogError($"You are trying to open level with index {levelIndex}.There is no such level");
            }

            if (levelIndex + 1 > _levels.Count)
            {
                Debug.LogError($"You are trying to open level with index {levelIndex}. _levels last index:{_levels.Count - 1}");
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

