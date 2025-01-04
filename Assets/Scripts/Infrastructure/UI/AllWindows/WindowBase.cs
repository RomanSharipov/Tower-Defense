using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.UI
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        
        public event Action<WindowBase> CloseButtonClicked;
        
        private void Awake()
        {
            OnAwake();
        }

        protected virtual void OnAwake()
        {
            _closeButton?.onClick.AddListener(() => CloseButtonClicked?.Invoke(this));
        }

        public virtual void Initialize()
        {
            
        }

        public void Setup()
        {
            
        }
    }
}
