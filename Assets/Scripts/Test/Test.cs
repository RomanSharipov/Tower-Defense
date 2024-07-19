using Assets.Scripts.Infrastructure;
using UnityEngine;
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

    [ContextMenu("EnterPauseState")]
    public void EnterPauseState()
    {
        _gameRoot.MainGameStatemachine.Enter<GameLoopState>();
    }
}
