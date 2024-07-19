using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Assets.Scripts.Infrastructure;
using Cysharp.Threading.Tasks;

public class AddressableProvider : IAssetProvider
{
    private readonly Dictionary<string, AsyncOperationHandle> _completedCache = new();
    private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new();

    public void Initialize() =>
        Addressables.InitializeAsync();

    public async UniTask<T> Load<T>(string key) where T : class
    {
        if (_completedCache.TryGetValue(key, out var completedHandle))
            return completedHandle.Result as T;

        var handle = Addressables.LoadAssetAsync<T>(key);

        return await RunWithCacheOnComplete(handle: handle, cacheKey: key);
    }

    public async UniTask<SceneInstance> LoadScene(SceneName sceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
        return await Addressables
            .LoadSceneAsync(
                key: sceneName.ToString(),
                loadMode: mode)
            .Task;
    }

    public void Release(string key)
    {
        if (!_handles.ContainsKey(key))
            return;

        foreach (var handle in _handles[key])
            Addressables.Release(handle);

        _completedCache.Remove(key);
        _handles.Remove(key);
    }

    public void Cleanup()
    {
        if (_handles.Count == 0)
            return;

        foreach (var resourceHandles in _handles.Values)
            foreach (var handle in resourceHandles)
                Addressables.Release(handle);

        _completedCache.Clear();
        _handles.Clear();
    }


    private async UniTask<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
    {
        handle.Completed += completeHandle =>
            _completedCache[cacheKey] = completeHandle;

        AddHandle(cacheKey, handle);
        return await handle.Task;
    }

    private void AddHandle(string key, AsyncOperationHandle handle)
    {
        if (!_handles.TryGetValue(key, out var resourceHandles))
        {
            resourceHandles = new List<AsyncOperationHandle>();
            _handles[key] = resourceHandles;
        }

        resourceHandles.Add(handle);
    }
}