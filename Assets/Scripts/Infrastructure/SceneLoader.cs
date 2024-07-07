using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Infrastructure
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask<Scene> Load(SceneName name, Action onLoaded = null)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name.ToString());
            
            await UniTask.WaitUntil(() => asyncOperation.isDone);

            onLoaded?.Invoke();
            
            return SceneManager.GetSceneByName(name.ToString());
        }
    }

    public enum SceneName 
    { 
        None,
        Menu
    }

}
