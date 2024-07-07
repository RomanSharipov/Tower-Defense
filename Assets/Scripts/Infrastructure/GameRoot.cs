using System;
using System.Collections.Generic;
using VContainer;

namespace Assets.Scripts.Infrastructure
{
    public class GameRoot
    {
        private GameStatemachine _mainGameStatemachine;

        public GameRoot(IObjectResolver objectResolver)
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
                objectResolver.Inject(state);
            }
            
            _mainGameStatemachine.Enter<BootstrapState>();
        }
    }
}