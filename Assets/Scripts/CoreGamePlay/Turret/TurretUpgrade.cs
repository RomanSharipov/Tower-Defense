using System;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    [Serializable]
    public class TurretUpgrade
    {
        [SerializeField] private TurretRotation[] _turretRotations;

        private int _currentTurretIndex = 0;
        private TurretRotation _currentTurretRotation;

        public TurretRotation CurrentUpgradeTurretRotation => _currentTurretRotation;

        public bool HasNextUpgrade => _currentTurretIndex < _turretRotations.Length - 1;

        
        public void LevelUp()
        {
            if (!HasNextUpgrade)
                return;
            
            _turretRotations[_currentTurretIndex].gameObject.SetActive(false);
            
            _currentTurretIndex++;
            
            _currentTurretRotation = _turretRotations[_currentTurretIndex];
            
            _currentTurretRotation.gameObject.SetActive(true);
        }

        public void Init()
        {
            for (int i = 0; i < _turretRotations.Length; i++)
            {
                _turretRotations[i].gameObject.SetActive(false);
            }

            _currentTurretIndex = 0;
            _currentTurretRotation = _turretRotations[_currentTurretIndex];
            _currentTurretRotation.gameObject.SetActive(true);
            
        }
    }
}
