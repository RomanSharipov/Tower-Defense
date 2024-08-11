using Assets.Scripts.CoreGamePlay.Level;
using Assets.Scripts.Extension;
using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Assets.Scripts.Infrastructure.Services
{
    public class LevelService : ILevelService
    {
        private readonly ISceneLoader _sceneLoader;

        [Inject]
        public LevelService(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public async UniTask<ILevelMain> LoadCurrentLevel()
        {
            Scene scene = await _sceneLoader.Load("Level_1");
            
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
            _sceneLoader.Unload("Level_1");
        }
    }
}
