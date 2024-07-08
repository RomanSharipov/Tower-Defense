using Assets.Scripts.Infrastructure;
using UnityEngine;
using VContainer;

public class Test : MonoBehaviour
{
    [Inject] private ISceneLoader sceneLoader;


    private void Awake()
    {
        
    }


    [ContextMenu("UnloadMenu")]
    public void UnloadMenu()
    {
        sceneLoader.Unload(SceneName.Menu);
    }
}
