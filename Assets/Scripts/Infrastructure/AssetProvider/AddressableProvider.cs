using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AddressableProvider : IAssetProvider
{
    private readonly Dictionary<string, AsyncOperationHandle> _completedCache = new();
    private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new();

    public void Initialize()
    {
        Addressables.InitializeAsync();
    }

    public async UniTask<T> Load<T>(AssetReference assetReference) where T : class
    {
        if (_completedCache.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle completedHandle))
            return completedHandle.Result as T;

        return await RunWithCacheOnComplete(Addressables.LoadAssetAsync<T>(assetReference), assetReference.AssetGUID);
    }

    public async UniTask<GameObject> Instantiate(AssetReference assetReference, Vector3 position)
    {
        return await Addressables.InstantiateAsync(assetReference, position,Quaternion.identity);
    }

    public async UniTask<GameObject> Instantiate(AssetReference assetReference)
    {
        return await Addressables.InstantiateAsync(assetReference);
    }

    public void Release(string key)
    {
        if (!_handles.ContainsKey(key))
            return;

        foreach (AsyncOperationHandle handle in _handles[key])
            Addressables.Release(handle);

        _completedCache.Remove(key);
        _handles.Remove(key);
    }

    public void Cleanup()
    {
        if (_handles.Count == 0)
            return;

        foreach (List<AsyncOperationHandle> resourceHandles in _handles.Values)
            foreach (AsyncOperationHandle handle in resourceHandles)
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

    private void AddHandle<T>(string key, AsyncOperationHandle<T> handle)
    {
        if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandles))
        {
            resourceHandles = new List<AsyncOperationHandle>();
            _handles[key] = resourceHandles;
        }

        resourceHandles.Add(handle);
    }
}