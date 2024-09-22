using Assets.Scripts.CoreGamePlay;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace CodeBase.Infrastructure.UI.Services
{
    public class BuildButton : MonoBehaviour
    {
        [SerializeField] private EventTrigger _buildButton;
        [SerializeField] private TurretId _turretId;
        
        [Inject] private IAppStateService _appStateService;
        
        private void OnBuildingButtonClick(BaseEventData arg0)
        {
            _appStateService.GoToBuildingTurretState(SetupBeforeBuilding);
        }

        private void SetupBeforeBuilding(BuildingTurretState state)
        {
            state.Setup(_turretId);
        }

        private void OnEnable()
        {
            _buildButton.AddListener(EventTriggerType.PointerDown,OnBuildingButtonClick);
        }
        
        private void OnDisable()
        {
            _buildButton.RemoveListener(EventTriggerType.PointerDown, OnBuildingButtonClick);
        }
    }
}
