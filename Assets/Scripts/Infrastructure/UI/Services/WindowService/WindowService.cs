using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
            CreateWindow(windowId).Forget();
        }

        public async UniTask Open<TWindow>(WindowId windowId,Action<TWindow> setupBeforeOpen) where TWindow : WindowBase
        {
            if (_openedWindows.ContainsKey(windowId))
            {
                Debug.LogError($"Window with name {windowId} already opened");
                return;
            }
            WindowBase window = await CreateWindow(windowId);
            setupBeforeOpen.Invoke((TWindow)window);
        }

        public bool NowIsOpen(WindowId windowId)
        {
            return _openedWindows.ContainsKey(windowId);
        }

        private async UniTask<WindowBase> CreateWindow(WindowId windowId)
        {
            try
            {
                WindowBase window = await _uiFactory.CreateWindow(windowId);
                if (window != null)
                {
                    _openedWindows.Add(windowId, window);
                    window.Initialize();
                    window.CloseButtonClicked += CloseWindow;
                }
                return window;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to create window with ID {windowId}: {ex.Message}");
                return null;
            }
        }

        public void CloseWindow(WindowId windowId)
        {
            if (_openedWindows.TryGetValue(windowId, out WindowBase window))
            {
                window.CloseButtonClicked -= CloseWindow;
                GameObject.Destroy(window.gameObject);
                _openedWindows.Remove(windowId);
            }
            else
            {
                Debug.LogWarning($"Attempted to close a window with ID {windowId} that was not open.");
            }
        }

        public void CloseWindowIfOpened(WindowId windowId)
        {
            if (NowIsOpen(windowId))
                CloseWindow(windowId);
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
