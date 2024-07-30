using VContainer;
using UnityEngine;
using CodeBase.Infrastructure.UI.Services;

namespace CodeBase.Infrastructure.UI
{
    public class MainMenu : WindowBase
    {
        [SerializeField] private GoToStateButton[] _buttons; 

        [Inject]
        public void Construct(IObjectResolver objectResolver)
        {
            foreach (GoToStateButton button in _buttons)
            {
                button.Construct(objectResolver.Resolve<GameRoot>().MainGameStatemachine);
            }
        }
    }
}
