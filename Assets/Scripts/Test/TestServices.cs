using CodeBase.Infrastructure.Services;
using UnityEngine;
using VContainer;

public class TestServices : MonoBehaviour
{
    [Inject] private ISceneLoader sceneLoader;
    
    [ContextMenu("UnloadLevel_1")]
    public void UnloadLevel_1()
    {
        sceneLoader.Unload("Level_1");
    }

    [ContextMenu("LoadLevel_1")]
    public void LoadLevel_1()
    {
        sceneLoader.Load("Level_1");
    }
}
