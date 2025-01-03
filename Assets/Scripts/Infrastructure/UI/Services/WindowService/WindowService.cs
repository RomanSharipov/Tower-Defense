using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.UI.Services
{
    public class WindowService : IWindowService
    {
        private IUIFactory _uiFactory;
        private Dictionary<Type,WindowBase> _openedWindows = new Dictionary<Type, WindowBase>();
        
        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }
        
        public async UniTask Open<TWindow>(Action<TWindow> setupBeforeOpen = null) where TWindow : WindowBase
        {
            if (_openedWindows.ContainsKey(typeof(TWindow)))
            {
                Debug.LogError($"Window with name {typeof(TWindow)} already opened");
                return;
            }
            TWindow window = await CreateWindow<TWindow>();
            setupBeforeOpen.Invoke(window);
        }

        public bool NowIsOpen<TWindow>()
        {
            return _openedWindows.ContainsKey(typeof(TWindow));
        }

        private async UniTask<TWindow> CreateWindow<TWindow>() where TWindow : WindowBase
        {
            try
            {
                TWindow window = await _uiFactory.CreateWindow<TWindow>();
                if (window != null)
                {
                    _openedWindows.Add(typeof(TWindow), window);
                    window.Initialize();
                    window.CloseButtonClicked += CloseWindowInternal;
                }
                return window;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to create window with ID {typeof(TWindow)}: {ex.Message}");
                return null;
            }
        }

        private void CloseWindowInternal(WindowBase window)
        {
            if (_openedWindows.TryGetValue(window.GetType(), out WindowBase windowInternal))
            {
                windowInternal.CloseButtonClicked -= CloseWindowInternal;
                GameObject.Destroy(windowInternal.gameObject);
                _openedWindows.Remove(window.GetType());
            }
            else
            {
                Debug.LogWarning($"Attempted to close a window with ID {window.GetType()} that was not open.");
            }
        }

        public void CloseWindow<TWindow>() where TWindow : WindowBase
        {
            if (_openedWindows.TryGetValue(typeof(TWindow), out WindowBase windowInternal))
            {
                windowInternal.CloseButtonClicked -= CloseWindowInternal;
                GameObject.Destroy(windowInternal.gameObject);
                _openedWindows.Remove(typeof(TWindow));
            }
            else
            {
                Debug.LogWarning($"Attempted to close a window with ID {typeof(TWindow)} that was not open.");
            }
        }
        
        public void CloseWindowIfOpened<TWindow>() where TWindow : WindowBase
        {
            if (NowIsOpen<TWindow>())
                CloseWindowInternal(_openedWindows[typeof(TWindow)]);
        }

        public void CloseAllWindows() 
        {
            foreach (WindowBase window in _openedWindows.Values)
            {
                window.CloseButtonClicked -= CloseWindowInternal;
                GameObject.Destroy(window.gameObject);
            }
            _openedWindows.Clear();
        }
    }
}
