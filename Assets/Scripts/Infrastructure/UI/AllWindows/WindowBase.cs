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
        [SerializeField] private GameObject[] _autoInjectGameObjects;
        
        [SerializeField] private Button _closeButton;
        [SerializeField] private WindowId _windowId;

        public event Action<WindowId> CloseButtonClicked;
        public WindowId WindowId => _windowId;

        [Inject]
        public void Construct(IObjectResolver objectResolver)
        {
            foreach (GameObject gameObject in _autoInjectGameObjects)
            {
                if (gameObject != this.gameObject)  
                {
                    objectResolver.InjectGameObject(gameObject);
                }
                else
                {
                    Debug.LogError($"{this.gameObject.name}: You cannot add an object with WindowBase to _autoInjectGameObjects, as this will result in recursive dependency injection. " +
                        "This will throw a StackOverflowException, as the object will try to inject itself infinitely.");
                }
            }
        }

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
