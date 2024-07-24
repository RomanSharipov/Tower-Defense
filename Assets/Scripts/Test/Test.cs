using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.UI.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

public class Test : MonoBehaviour
{
    [Inject] private ISceneLoader sceneLoader;
    [Inject] private GameRoot _gameRoot;


    private void Awake()
    {
        
    }


    [ContextMenu("UnloadMenu")]
    public void UnloadMenu()
    {
        sceneLoader.Unload(SceneName.Menu);
    }

    [ContextMenu("LoadAsset")]

    public void LoadAsset()
    {
        LoadAssetAsync().Forget();
    }

    public async UniTask LoadAssetAsync()
    {
        GameObject newGameobject = await Addressables.LoadAssetAsync<GameObject>("33b67f947d8defd42b1fc6fd90ce5e7b");
        Instantiate(newGameobject);
    }
}
