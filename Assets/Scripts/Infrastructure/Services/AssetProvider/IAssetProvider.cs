using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
    public interface IAssetProvider
    {
        public UniTask<T> Load<T>(AssetReference key) where T : class;
        public void Release(string key);
        public void Cleanup();
        public UniTask Initialize();
        public UniTask<Scene> LoadScene(AssetReference assetReference);
        public void ReleaseScene(AssetReference assetReference);
    }
}