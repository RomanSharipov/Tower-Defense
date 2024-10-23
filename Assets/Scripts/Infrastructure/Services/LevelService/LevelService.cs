using Assets.Scripts.CoreGamePlay;
using CodeBase.Helpers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace CodeBase.Infrastructure.Services
{
    public class LevelService : ILevelService
    {
        private readonly ISceneLoader _sceneLoader;

        private int _currentLevel;

        [Inject]
        public LevelService(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public async UniTask<ILevelMain> LoadCurrentLevel()
        {
            int currentLevel = 1;
            
            Scene scene = await _sceneLoader.LoadLevel(currentLevel);
            
            _currentLevel = currentLevel;
            if (scene.TryGetRoot(out ILevelMain result))
            {
                return result;
            }
            else 
            {
                Debug.LogError($"ILevelMain dont found in {scene.name} scene. Add {nameof(LevelMainMonoBehaviour)} object to {scene.name} scene");
                return null; 
            }
        }

        public void UnLoadCurrentLevel()
        {
            _sceneLoader.UnloadLevel(_currentLevel);
        }
    }
}
