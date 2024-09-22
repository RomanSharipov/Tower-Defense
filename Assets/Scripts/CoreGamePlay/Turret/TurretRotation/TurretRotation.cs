using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class TurretRotation : MonoBehaviour
    {
        [SerializeField] private RotationX _rotationX; 
        [SerializeField] private RotationY _rotationY; 
        [SerializeField] private TurretBase _turret; 
        
        private Vector3 _directionToTarget;
        public Vector3 DirectionToTarget => _directionToTarget; 
        
        public void RotateTurretTowardsTarget(float rotationSpeed)
        {
            if (_turret.CurrentTarget == null) 
                return;

            _directionToTarget = _turret.CurrentTarget.Position - _rotationX.transform.position;

            _directionToTarget.Normalize();
            
            _rotationY.RotateTowards(_directionToTarget, rotationSpeed);
            _rotationX.RotateTowards(_directionToTarget, rotationSpeed);
        }

        public bool IsRotationComplete(float thresholdAngle = 1f)
        {
            if (_turret.CurrentTarget == null)
                return false;
            
            Vector3 normalizedDirectionToTarget = _directionToTarget.normalized;
            
            float angleDifference = Vector3.Angle(_rotationX.transform.forward, normalizedDirectionToTarget);
            
            return angleDifference <= thresholdAngle;
        }
    }
}
