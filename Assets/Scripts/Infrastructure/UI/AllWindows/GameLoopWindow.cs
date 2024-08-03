using VContainer;
using UnityEngine;
using CodeBase.Infrastructure.UI.Services;

namespace CodeBase.Infrastructure.UI
{
    public class GameLoopWindow : WindowBase
    {
        [SerializeField] private GoToStateButton[] _buttons; 

        [Inject]
        public void Construct(IObjectResolver objectResolver)
        {
            foreach (GoToStateButton button in _buttons)
            {
                button.Construct(objectResolver.Resolve<GameRoot>().MainGameStatemachine, 
                    objectResolver.Resolve<GameRoot>().GameLoopStatemachine);
            }
        }
    }
}
