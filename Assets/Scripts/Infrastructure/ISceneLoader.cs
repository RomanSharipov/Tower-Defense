using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Infrastructure
{
    public interface ISceneLoader
    {
        public UniTask<Scene> Load(SceneName name, Action onLoaded = null);
        public UniTask Unload(SceneName name, Action onUnloaded = null);
    }
}