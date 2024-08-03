using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Infrastructure.UI.Services
{
    public class WindowService : IWindowService
    {
        private IUIFactory _uiFactory;
        private Dictionary<WindowId,WindowBase> _openedWindows = new Dictionary<WindowId, WindowBase>();
        
        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public async UniTask Open(WindowId windowId)
        {
            if (_openedWindows.ContainsKey(windowId))
            {
                Debug.LogError($"Window with name {windowId} already opened");
                return;
            }
            
            switch (windowId)
            {
                case WindowId.None:
                    break;

                case WindowId.Shop:
                    _uiFactory.CreateShop();
                    break;

                case WindowId.MainMenu:
                    MainMenu menu = await _uiFactory.CreateMainMenu();
                    _openedWindows.Add(windowId, menu);
                    menu.CloseButtonClicked += CloseWindow;
                    break;
            }
        }

        public void CloseWindow(WindowBase window)
        {
            window.CloseButtonClicked -= CloseWindow;
            _openedWindows.Remove(window.WindowId);
            GameObject.Destroy(window.gameObject);
        }

        public void CloseAllWindows() 
        {
            foreach (WindowBase window in _openedWindows.Values)
            {
                CloseWindow(window);
            }
        }
    }
}
