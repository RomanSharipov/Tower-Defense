using Assets.Scripts.Infrastructure;
using UnityEngine;
using VContainer;

public class TestServices : MonoBehaviour
{
    [Inject] private ISceneLoader sceneLoader;
    
    [ContextMenu("UnloadMenu")]
    public void UnloadMenu()
    {
        sceneLoader.Unload(SceneName.Menu);
    }

    [ContextMenu("LoadMenu")]
    public void LoadMenu()
    {
        sceneLoader.Load(SceneName.Menu);
    }
}
