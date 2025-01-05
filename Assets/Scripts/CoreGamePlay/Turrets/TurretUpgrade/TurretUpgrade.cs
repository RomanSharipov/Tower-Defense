using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class TurretUpgrade
    {
        private int _currentLevelIndex;
        private int _maxLevel;
        
        public List<IUpgradeable> _upgradeables = new List<IUpgradeable>();
        
        public bool HasNextUpgrade => _currentLevelIndex < _maxLevel - 1;
        public int CurrentLevelIndex => _currentLevelIndex;

        public event Action TurretUpgraded;
        
        public TurretUpgrade(int maxLevel)
        {
            _maxLevel = maxLevel;
        }

        public void RegisterUpgradeable(IUpgradeable upgradeable)
        {
            _upgradeables.Add(upgradeable);
        }

        public void LevelUp()
        {
            if (!HasNextUpgrade)
                return;
            
            _currentLevelIndex++;

            UpdateLevel(_currentLevelIndex);
        }

        public void ResetLevel()
        {
            _currentLevelIndex = 0;
            UpdateLevel(_currentLevelIndex);
        }

        private void UpdateLevel(int index)
        {
            foreach (IUpgradeable upgradeable in _upgradeables)
            {
                upgradeable.SetLevel(index);
            }
            TurretUpgraded?.Invoke();
        }
    }
}
