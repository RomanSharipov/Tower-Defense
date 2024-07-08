using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.Infrastructure
{
    public class MenuState : IState
    {
        private GameStatemachine _mainGameStatemachine;
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public MenuState(GameStatemachine mainGameStatemachine)
        {
            _mainGameStatemachine = mainGameStatemachine;
        }

        public async UniTask Enter()
        {
            Debug.Log($"MenuState Enter");
            await _sceneLoader.Load(SceneName.Menu);
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}
