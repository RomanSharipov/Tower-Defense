using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using VContainer;

namespace Assets.Scripts.Infrastructure
{
    public class SceneLoader : ISceneLoader
    {
        private readonly Dictionary<SceneName, AssetReference> _sceneReferences;
        private readonly IAssetProvider _assetProvider;
        private readonly Dictionary<SceneName, AsyncOperationHandle<SceneInstance>> _handles = 
            new Dictionary<SceneName, AsyncOperationHandle<SceneInstance>>();

        [Inject]
        public SceneLoader(Dictionary<SceneName, AssetReference> sceneReferences,IAssetProvider assetProvider)
        {
            _sceneReferences = sceneReferences;
            _assetProvider = assetProvider;

            foreach (SceneName sceneName in _sceneReferences.Keys)
            {
                _handles.Add(sceneName, default);
            }
        }

        public async UniTask<Scene> Load(SceneName name, Action onLoaded = null)
        {
            if (!_sceneReferences.TryGetValue(name, out AssetReference sceneReference))
            {
                Debug.LogError($"Scene {name} not found in references.");
                return default;
            }
            
            AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneReference, LoadSceneMode.Additive);

            await handle.Task;

            _handles[name] = handle;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                onLoaded?.Invoke();
                return handle.Result.Scene;
            }
            else
            {
                Debug.LogError($"Failed to load scene: {name}");
                return default;
            }
        }

        public async UniTask Unload(SceneName name, Action onUnloaded = null)
        {
            if (!_handles.TryGetValue(name, out AsyncOperationHandle<SceneInstance> handle))
            {
                Debug.LogError($"Scene {name} is not loaded");
                return;
            }
            
            if (handle.IsValid())
            {
                Addressables.Release(handle);
                _handles[name] = default;
                onUnloaded?.Invoke();
            }
        }
    }

    public enum SceneName
    {
        None,
        Menu
    }
}