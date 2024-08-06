using System;
using System.Collections.Generic;
using VContainer;

namespace CodeBase.Infrastructure
{
    public class GameRoot
    {
        private GameStatemachine _mainGameStatemachine;
        private GameStatemachine _gameLoopStatemachine;
        private IObjectResolver _objectResolver;

        public GameStatemachine MainGameStatemachine => _mainGameStatemachine;
        public GameStatemachine GameLoopStatemachine => _gameLoopStatemachine;

        public GameRoot()
        {
            
        }

        public void Construct(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }
        
        public void Start()
        {
            _mainGameStatemachine = new GameStatemachine();
            GameLoopState gameLoopState = new GameLoopState(_mainGameStatemachine);
            BootstrapState bootstrapState = new BootstrapState(_mainGameStatemachine);
            MenuState menuState = new MenuState(_mainGameStatemachine);

            _gameLoopStatemachine = gameLoopState.SubStatemachine;

            Dictionary<Type, IState> states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = bootstrapState,
                [typeof(MenuState)] = menuState,
                [typeof(GameLoopState)] = gameLoopState,
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