using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEditor.PackageManager.UI;
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
                    
                    break;

                case WindowId.MainMenu:
                    CreateWindow<MainMenu>(windowId).Forget();
                    break;
                case WindowId.GameLoopWindow:
                    CreateWindow<GameLoopWindow>(windowId).Forget();
                    break;
            }
        }
        private async UniTask<TypeWindow> CreateWindow<TypeWindow>(WindowId windowId) where TypeWindow : WindowBase
        {
            TypeWindow window = await _uiFactory.CreateWindow<TypeWindow>(windowId);
            _openedWindows.Add(windowId, window);
            window.CloseButtonClicked += CloseWindow;
            return window;
        }
        public void CloseWindow(WindowId windowId)
        {
            _openedWindows[windowId].CloseButtonClicked -= CloseWindow;
            GameObject.Destroy(_openedWindows[windowId].gameObject);
            _openedWindows.Remove(windowId);
        }

        public void CloseAllWindows() 
        {
            foreach (WindowBase window in _openedWindows.Values)
            {
                window.CloseButtonClicked -= CloseWindow;
                GameObject.Destroy(window.gameObject);
            }
            _openedWindows.Clear();
        }
    }
}
