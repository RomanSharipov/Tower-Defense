using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.UI.Services
{
    public class BuildButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        private GameStatemachine _gameLoopStatemachine;

        public void Construct(GameStatemachine gameLoopStatemachine)
        {
            _gameLoopStatemachine = gameLoopStatemachine;
        }

        private void Awake()
        {
            _button.OnClickAsObservable().Subscribe(_ =>
            {
                _gameLoopStatemachine.Enter<BuildingState>(buildingState =>
                {

                });
            }).AddTo(this);
        }
    }
}
