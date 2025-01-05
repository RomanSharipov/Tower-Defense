using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Assets.Scripts.CoreGamePlay;
using System;
using CodeBase.Infrastructure.UI.Services;
using VContainer;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.UI
{
    public class TurretContextMenu : WindowBase
    {
        [SerializeField] private Button _upgrageTurretButton;
        [SerializeField] private Button _removeTurretButton;

        private TurretBase _turret;
        [Inject] private IWindowService _windowService;
        [Inject] private ITurretPriceProvider _turretPriceProvider;
        [Inject] private IPlayerResourcesService _playerResourcesService;
        
        public override void Initialize()
        {
            base.Initialize();
            UpdateUpgrageButtonState();
            
            _upgrageTurretButton.OnClickAsObservable().Subscribe(_ =>
            {
                int price = _turretPriceProvider.GetUpgradePrice(_turret.TurretId, _turret.TurretUpgrade.CurrentLevelIndex);

                if (_playerResourcesService.TryDecreaseResource(ResourcesType.Money, price))
                {
                    _turret.LevelUp();
                    UpdateUpgrageButtonState();
                }


            }).AddTo(this);

            _removeTurretButton.OnClickAsObservable().Subscribe(_ =>
            {
                _turret.RemoveSelf();
                _windowService.CloseWindow<TurretContextMenu>();
            }).AddTo(this);
        }

        private void UpdateUpgrageButtonState()
        {
            _upgrageTurretButton.gameObject.SetActive(_turret.TurretUpgrade.HasNextUpgrade);
        }

        public void Setup(TurretBase turret)
        {
            _turret = turret;
        }
    }
}
