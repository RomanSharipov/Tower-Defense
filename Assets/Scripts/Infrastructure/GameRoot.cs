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
            _mainGameStatemachine = new GameStatemachine(new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(),
                [typeof(MenuState)] = new MenuState(),
                [typeof(GameLoopState)] = new GameLoopState(),
            }, objectResolver);
            
            _mainGameStatemachine.Enter<BootstrapState>();
        }
    }
}