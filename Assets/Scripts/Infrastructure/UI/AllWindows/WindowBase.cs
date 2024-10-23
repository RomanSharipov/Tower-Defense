using System;
using CodeBase.Infrastructure.UI.Services;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.UI
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private WindowId _windowId;

        public event Action<WindowId> CloseButtonClicked;
        public WindowId WindowId => _windowId;
        
        private void Awake()
        {
            OnAwake();
        }

        protected virtual void OnAwake()
        {
            _closeButton?.onClick.AddListener(() => CloseButtonClicked?.Invoke(_windowId));
        }
    }
}
