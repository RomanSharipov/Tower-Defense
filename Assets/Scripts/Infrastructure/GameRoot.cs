using System;
using System.Collections.Generic;
using VContainer;

namespace Assets.Scripts.Infrastructure
{
    public class GameRoot
    {
        [Inject] private IObjectResolver _objectResolver;

        private GameStatemachine _mainGameStatemachine;

        public GameRoot()
        {
            _mainGameStatemachine = new GameStatemachine(new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(),
                [typeof(MenuState)] = new MenuState(),
                [typeof(GameLoopState)] = new GameLoopState(_objectResolver),
            }, _objectResolver);
            _mainGameStatemachine.Enter<BootstrapState>();
        }
    }
}