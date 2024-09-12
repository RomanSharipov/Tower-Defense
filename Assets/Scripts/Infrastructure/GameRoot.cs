using System;
using System.Collections.Generic;
using VContainer;

namespace CodeBase.Infrastructure
{
    public class GameRoot
    {
        private GameStatemachine _mainGameStatemachine;
        private GameStatemachine _gameLoopStatemachine;

        [Inject] private IObjectResolver _objectResolver;
        [Inject] private GameLoopState _gameLoopState;
        [Inject] private BootstrapState _bootstrapState;
        [Inject] private MenuState _menuState;

        public GameStatemachine MainGameStatemachine => _mainGameStatemachine;
        public GameStatemachine GameLoopStatemachine => _gameLoopStatemachine;
        
        public void CreateStatemachine()
        {
            //_mainGameStatemachine = new GameStatemachine();
            //GameLoopState gameLoopState = new GameLoopState(_mainGameStatemachine);
            //BootstrapState bootstrapState = new BootstrapState(_mainGameStatemachine);
            //MenuState menuState = new MenuState(_mainGameStatemachine);

            //_gameLoopStatemachine = gameLoopState.SubStatemachine;

            //Dictionary<Type, IState> states = new Dictionary<Type, IState>()
            //{
            //    [typeof(BootstrapState)] = bootstrapState,
            //    [typeof(MenuState)] = menuState,
            //    [typeof(GameLoopState)] = gameLoopState,
            //};

            //_mainGameStatemachine.SetStates(states);

            //foreach (IState state in _mainGameStatemachine.States.Values)
            //{
            //    _objectResolver.Inject(state);
            //}
        }

        public void StartGame()
        {
            _mainGameStatemachine.Enter<BootstrapState>();
        }
    }
}