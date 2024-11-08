using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace CodeBase.Infrastructure.UI
{
    public class TurretContextMenu : WindowBase
    {
        [SerializeField] private Button _upgrageTurretButton;
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _upgrageTurretButton.OnClickAsObservable().Subscribe(_ =>
            {
                
            }).AddTo(this);
        }
    }
}
