using System.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Assets.Scripts.Infrastructure;
using Cysharp.Threading.Tasks;

public interface IAssetProvider
{
    public UniTask<T> Load<T>(string key) where T : class;
    public UniTask<SceneInstance> LoadScene(SceneName sceneName, LoadSceneMode mode = LoadSceneMode.Single);
    public void Release(string key);
    public void Cleanup();
}