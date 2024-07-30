using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.UI.Services
{
    public class WindowService : IWindowService
    {
        private IUIFactory _uiFactory;
        private HashSet<WindowId> _openedWindows = new HashSet<WindowId>();
        
        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(WindowId windowId)
        {
            if (_openedWindows.Contains(windowId))
            {
                Debug.LogError($"Window with name {windowId} already opened");
                return;
            }
            else
            {
                _openedWindows.Add(windowId);
            }

            switch (windowId)
            {
                case WindowId.None:
                    break;

                case WindowId.Shop:
                    _uiFactory.CreateShop();
                    break;

                case WindowId.MainMenu:
                    _uiFactory.CreateMainMenu();
                    break;
            }
        }

        public void CloseAllWindows() 
        {
            _uiFactory.DestroyAllWindows();
        }
    }
}
