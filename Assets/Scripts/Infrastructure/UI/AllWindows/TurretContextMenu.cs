using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Assets.Scripts.CoreGamePlay;
using System;

namespace CodeBase.Infrastructure.UI
{
    public class TurretContextMenu : WindowBase
    {
        [SerializeField] private Button _upgrageTurretButton;

        private TurretBase _turret;
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _upgrageTurretButton.OnClickAsObservable().Subscribe(_ =>
            {
                _turret.LevelUpTest();
            }).AddTo(this);
        }

        public void Setup(TurretBase turret)
        {
            _turret = turret;
        }
    }
}
