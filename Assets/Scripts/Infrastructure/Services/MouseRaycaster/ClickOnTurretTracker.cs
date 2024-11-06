using System;
using Assets.Scripts.CoreGamePlay;
using UniRx;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure.Services
{
    public class ClickOnTurretTracker : IClickOnTurretTracker
    {
        [Inject] Camera _camera;
        [Inject] IInputService _inputService;
        [Inject] ILayerMaskProvider _layerMaskProvider;

        private CompositeDisposable _compositeDisposable = new();
        
        private Collider _currentCollider;
        private TurretBase _currentTurret;
        private Subject<TurretBase> _clickOnTurret = new();

        public IObservable<TurretBase> ClickOnTurret => _clickOnTurret;

        public void StartTracking()
        {
            _inputService.GetKeyDown.Subscribe(_ =>
            {
                Ray ray = _camera.ScreenPointToRay(_inputService.MousePosition);
                OnKeyDown(ray);
                
            }).AddTo(_compositeDisposable);

            _inputService.GetKeyUp.Subscribe(_ =>
            {
                Ray ray = _camera.ScreenPointToRay(_inputService.MousePosition);
                OnKeyUp(ray);
            }).AddTo(_compositeDisposable);
        }

        public void EndTracking()
        {
            _compositeDisposable.Dispose();
        }

        private void OnKeyDown(Ray ray)
        {
            if (TryGetCollider(ray, out Collider collider))
            {
                if (collider.TryGetComponent(out TurretBase turret))
                {
                    _currentCollider = collider;
                    _currentTurret = turret;
                    
                }
            }
        }
        
        private void OnKeyUp(Ray ray)
        {
            if (TryGetCollider(ray, out Collider collider))
            {
                if (_currentCollider == collider)
                {
                    _clickOnTurret.OnNext(_currentTurret);
                }
            }
            _currentTurret = null;
            _currentCollider = null;
            return;
        }

        private bool TryGetCollider(Ray ray, out Collider collider)
        {
            collider = null;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, _layerMaskProvider.Turret))
            {
                collider = hit.collider;
                return true;
            }
            return false;
        }
    }
}