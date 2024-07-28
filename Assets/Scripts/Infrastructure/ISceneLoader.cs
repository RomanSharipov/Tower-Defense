using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Infrastructure
{
    public interface ISceneLoader
    {
        public UniTask<Scene> Load(SceneName name, Action onLoaded = null);
        public void Unload(SceneName name);
    }
}