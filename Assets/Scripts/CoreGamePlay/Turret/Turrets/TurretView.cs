using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class TurretView : MonoBehaviour, IUpgradeable
    {
        [SerializeField] private TurretRotation[] _turretRotations;
        [SerializeField] private TurretRotation _currentTurretRotation;

        public TurretRotation CurrentTurretRotation => _currentTurretRotation;

        public void SetLevel(int level)
        {
            if (level == 0) 
            {
                foreach (TurretRotation turretRotation in _turretRotations)
                {
                    turretRotation.gameObject.SetActive(false);
                }
            }

            _currentTurretRotation?.gameObject.SetActive(false);
            _currentTurretRotation = _turretRotations[level];
            _currentTurretRotation.gameObject.SetActive(true);
        }
        
        public void ResetLevel()
        {
            foreach (TurretRotation turretRotation in _turretRotations)
            {
                turretRotation.gameObject.SetActive(false);
            }
        }
    }
}
