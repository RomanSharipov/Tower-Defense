using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using VContainer;

namespace Assets.Scripts.Infrastructure
{
    public class GameLoopState : IState
    {
        [Inject] 
        private IObjectResolver _resolver;

        private GameStatemachine _subStatemachine;

        public GameLoopState()
        {
            _subStatemachine = new GameStatemachine(new Dictionary<Type, IState>()
            {
                [typeof(PauseState)] = new PauseState()
            },_resolver);
        }

        public UniTask Enter()
        {
            throw new System.NotImplementedException();
        }

        public UniTask Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}
