using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Services
{
    public interface ISceneLoader
    {
        public UniTask<Scene> Load(string name, Action onLoaded = null);
        public UniTask<Scene> LoadLevel(int levelNumber, Action onLoaded = null);
        public void Unload(string name);
        public void UnloadLevel(int levelNumber);
    }
}