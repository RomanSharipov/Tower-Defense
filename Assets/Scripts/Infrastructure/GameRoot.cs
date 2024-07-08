using System;
using System.Collections.Generic;
using VContainer;

namespace Assets.Scripts.Infrastructure
{
    public class GameRoot
    {
        private GameStatemachine _mainGameStatemachine;
        private IObjectResolver _objectResolver;

        public GameRoot(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }
        
        public void Start()
        {
            _mainGameStatemachine = new GameStatemachine();

            Dictionary<Type, IState> states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(_mainGameStatemachine),
                [typeof(MenuState)] = new MenuState(_mainGameStatemachine),
                [typeof(GameLoopState)] = new GameLoopState(_mainGameStatemachine),
            };

            _mainGameStatemachine.SetStates(states);

            foreach (IState state in _mainGameStatemachine.States.Values)
            {
                _objectResolver.Inject(state);
            }

            _mainGameStatemachine.Enter<BootstrapState>();
        }
    }
}