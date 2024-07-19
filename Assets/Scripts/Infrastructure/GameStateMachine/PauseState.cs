
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class PauseState : IState
    {
        public UniTask Enter()
        {
            Debug.Log($"PauseState Enter");
            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}
