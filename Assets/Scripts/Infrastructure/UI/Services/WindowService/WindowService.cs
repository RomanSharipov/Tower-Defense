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

        public bool NowIsOpen(WindowId windowId)
        {
            return _openedWindows.ContainsKey(windowId);
        }

        private async UniTask CreateWindow(WindowId windowId) 
        {
            WindowBase window = await _uiFactory.CreateWindow(windowId);
            _openedWindows.Add(windowId, window);
            window.CloseButtonClicked += CloseWindow;
        }

        public void CloseWindow(WindowId windowId)
        {
            _openedWindows[windowId].CloseButtonClicked -= CloseWindow;
            GameObject.Destroy(_openedWindows[windowId].gameObject);
            _openedWindows.Remove(windowId);
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
