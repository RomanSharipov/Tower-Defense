using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class TurretRotation : MonoBehaviour
    {
        [SerializeField] private RotationX rotationX; 
        [SerializeField] private RotationY rotationY; 
        
        [SerializeField] private Transform _centerRotation; 

        private Vector3 _directionToTarget;
        [SerializeField] private TurretBase _turret; 
        public Vector3 DirectionToTarget => _directionToTarget; 
        
        public void RotateTurretTowardsTarget(float rotationSpeed)
        {
            if (_turret.CurrentTarget == null) 
                return;

            _directionToTarget = _turret.CurrentTarget.Position - _centerRotation.position;

            _directionToTarget.Normalize();
            
            rotationY.RotateTowards(_directionToTarget, rotationSpeed);
            rotationX.RotateTowards(_directionToTarget, rotationSpeed);
        }

        public bool IsRotationComplete(float thresholdAngle = 1f)
        {
            if (_turret.CurrentTarget == null)
                return false;
            
            Vector3 normalizedDirectionToTarget = _directionToTarget.normalized;
            
            float angleDifference = Vector3.Angle(rotationX.transform.forward, normalizedDirectionToTarget);
            
            return angleDifference <= thresholdAngle;
        }
    }
}
