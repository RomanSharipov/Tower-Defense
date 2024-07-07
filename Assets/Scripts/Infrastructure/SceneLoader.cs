using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Infrastructure
{
    public class SceneLoader : ISceneLoader
    {
        private readonly Dictionary<SceneName, AssetReference> _sceneReferences;

        public SceneLoader(Dictionary<SceneName, AssetReference> sceneReferences)
        {
            _sceneReferences = sceneReferences;
        }

        public async UniTask<Scene> Load(SceneName name, Action onLoaded = null)
        {
            if (!_sceneReferences.TryGetValue(name, out AssetReference sceneReference))
            {
                Debug.LogError($"Scene {name} not found in references.");
                return default;
            }

            AsyncOperationHandle<SceneInstance> handle = sceneReference.LoadSceneAsync(LoadSceneMode.Single);
            await handle.Task;

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
    }

    public enum SceneName
    {
        None,
        Menu
    }
}