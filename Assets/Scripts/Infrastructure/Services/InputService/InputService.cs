using System;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Services
{
    public class InputService : IInitializable,IInputService, IDisposable
    {
        private readonly Subject<Unit> _getKeyDown = new Subject<Unit>();
        private readonly Subject<Unit> _getKeyUp = new Subject<Unit>();

        private Vector3 _mousePosition = new Vector3();
        
        public IObservable<Unit> GetKeyDown => _getKeyDown;
        public IObservable<Unit> GetKeyUp => _getKeyUp;
        public Vector3 MousePosition => _mousePosition;

        private IDisposable _inputSubscription;
        
        public void Dispose()
        {
            _inputSubscription?.Dispose();
            _getKeyDown?.Dispose();
            _getKeyUp?.Dispose();
        }

        public void Initialize()
        {
            _inputSubscription = Observable.EveryUpdate().Subscribe(_ =>
            {
                _mousePosition = Input.mousePosition;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                    _getKeyDown.OnNext(Unit.Default);

                if (Input.GetKeyUp(KeyCode.Mouse0))
                    _getKeyUp.OnNext(Unit.Default);
            });
        }
    }
}
