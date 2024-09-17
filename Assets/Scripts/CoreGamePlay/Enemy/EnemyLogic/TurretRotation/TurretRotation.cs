using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class TurretRotation : MonoBehaviour
    {
        [SerializeField] private RotationX rotationX; 
        [SerializeField] private RotationY rotationY; 
        [SerializeField] private float rotationSpeed = 5f; 
        [SerializeField] private Transform _centerRotation; 
        public TurretBase _turret; 
        

        private void Update()
        {
            if (_turret.CurrentTarget != null)
            {
                RotateTurretTowardsTarget();
            }
        }

        private void RotateTurretTowardsTarget()
        {
            Vector3 directionToTarget = _turret.CurrentTarget.transform.position - _centerRotation.position;

            
            directionToTarget.Normalize();

            
            rotationY.RotateTowards(directionToTarget, rotationSpeed);
            rotationX.RotateTowards(directionToTarget, rotationSpeed);
        
        }
    }
}
