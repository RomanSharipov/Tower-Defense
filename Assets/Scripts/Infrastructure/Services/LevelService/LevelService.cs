using CodeBase.Infrastructure.Services;
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

        public void LoadCurrentLevel()
        {
            _sceneLoader.Load("Level_1");
        }

        public void UnLoadCurrentLevel()
        {
            _sceneLoader.Unload("Level_1");
        }
    }
}
