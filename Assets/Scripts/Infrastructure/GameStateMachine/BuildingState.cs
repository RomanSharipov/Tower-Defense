using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure
{
    public class BuildingState : IState
    {
        GameObject _turretTemplate;
        Camera _camera;


        [Inject]
        public void Construct(Camera camera)
        {
            _camera = camera;
        }

        public UniTask Enter()
        {
            
            return UniTask.CompletedTask;
        }

        public void Costruct(GameObject turretTemplate)
        {
            _turretTemplate = turretTemplate;
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}
