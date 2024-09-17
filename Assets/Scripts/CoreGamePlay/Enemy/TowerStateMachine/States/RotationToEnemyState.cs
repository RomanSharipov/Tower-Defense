using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class RotationToEnemyState : StateBase
    {
        private readonly TurretRotation _turretRotation;
        private float _speed = 3.0f;

        public RotationToEnemyState(TurretBase turret, TurretRotation turretRotation) : base(turret)
        {
            _turretRotation = turretRotation;
        }

        public override void Enter() { /* Логика начала поворота */ }

        public override void UpdateState() 
        {
            Debug.Log("RotationToEnemyState UpdateState");
            _turretRotation.RotateTurretTowardsTarget(_speed);
        }
        public override void Exit() { /* Логика завершения поворота */ }
    }
}
