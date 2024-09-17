using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class TurretRotation : MonoBehaviour
    {
        [SerializeField] private RotationX rotationX; 
        [SerializeField] private RotationY rotationY; 
        
        [SerializeField] private Transform _centerRotation; 

        public TurretBase _turret; 
        
        public void RotateTurretTowardsTarget(float rotationSpeed)
        {
            if (_turret.CurrentTarget == null) 
                return;

            Vector3 directionToTarget = _turret.CurrentTarget.Position - _centerRotation.position;
            
            directionToTarget.Normalize();
            
            rotationY.RotateTowards(directionToTarget, rotationSpeed);
            rotationX.RotateTowards(directionToTarget, rotationSpeed);
        
        }
    }
}
