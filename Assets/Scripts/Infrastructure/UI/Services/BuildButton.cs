using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VContainer;

namespace CodeBase.Infrastructure.UI.Services
{
    public class BuildButton : MonoBehaviour
    {
        [SerializeField] private EventTrigger _buildButton;
        
        private GameStatemachine _gameLoopStatemachine;

        [Inject]
        public void Construct(GameRoot gameRoot)
        {
            _gameLoopStatemachine = gameRoot.GameLoopStatemachine;
        }

        private void OnBuildingButtonClick(BaseEventData arg0)
        {
            _gameLoopStatemachine.Enter<BuildingTurretState>(buildingState =>
            {

            });
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
