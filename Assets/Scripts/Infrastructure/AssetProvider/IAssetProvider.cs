using System.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Assets.Scripts.Infrastructure;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine;

public interface IAssetProvider
{
    public UniTask<T> Load<T>(AssetReference key) where T : class;
    public void Release(string key);
    public void Cleanup();
    public void Initialize();
    public UniTask<GameObject> Instantiate(AssetReference assetReference, Vector3 position);
    public UniTask<GameObject> Instantiate(AssetReference assetReference);
}