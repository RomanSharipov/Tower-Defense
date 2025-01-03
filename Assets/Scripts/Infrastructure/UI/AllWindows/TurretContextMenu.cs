using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Assets.Scripts.CoreGamePlay;
using System;
using CodeBase.Infrastructure.UI.Services;
using VContainer;

namespace CodeBase.Infrastructure.UI
{
    public class TurretContextMenu : WindowBase
    {
        [SerializeField] private Button _upgrageTurretButton;
        [SerializeField] private Button _removeTurretButton;

        private TurretBase _turret;
        [Inject] private IWindowService _windowService;

        protected override void OnAwake()
        {
            base.OnAwake();

            _upgrageTurretButton.OnClickAsObservable().Subscribe(_ =>
            {
                _turret.LevelUpTest();
            }).AddTo(this);

            _removeTurretButton.OnClickAsObservable().Subscribe(_ =>
            {
                _turret.RemoveSelf();
                _windowService.CloseWindow<TurretContextMenu>();
            }).AddTo(this);
        }

        public void Setup(TurretBase turret)
        {
            _turret = turret;
        }
    }
}
