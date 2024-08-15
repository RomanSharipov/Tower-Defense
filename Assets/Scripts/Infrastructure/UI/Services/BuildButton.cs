using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.UI.Services
{
    public class BuildButton : MonoBehaviour
    {
        [SerializeField] private EventTrigger _buildButton;
        
        private GameStatemachine _gameLoopStatemachine;

        public void Construct(GameStatemachine gameLoopStatemachine)
        {
            _gameLoopStatemachine = gameLoopStatemachine;
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
