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
        [Inject] private readonly ISceneLoader _sceneLoader;

        private int _currentLevel = 2;
        
        public void IncreaseCurrentLevel()
        {
            _currentLevel++;
        }

        public async UniTask<ISceneInitializer> LoadCurrentLevel()
        {
            Scene scene = await _sceneLoader.LoadLevel(_currentLevel);
            
            if (scene.TryGetRoot(out ISceneInitializer result))
            {
                return result;
            }
            else 
            {
                Debug.LogError($"ILevelMain dont found in {scene.name} scene. Add {nameof(SceneInitializer)} object to {scene.name} scene");
                return null; 
            }
        }

        public void UnLoadCurrentLevel()
        {
            _sceneLoader.UnloadLevel(_currentLevel);
        }
    }
}
