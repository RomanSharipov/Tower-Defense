using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class RotationToEnemyState : StateBase
    {
        private readonly TurretUpgrade _turretUpgrade;
        private float _speed = 3.0f;

        public RotationToEnemyState(TurretBase turret, TurretUpgrade turretUpgrade) : base(turret)
        {
            _turretUpgrade = turretUpgrade;
        }

        public override void Enter() { /* Логика начала поворота */ }

        public override void UpdateState() 
        {
            _turretUpgrade.CurrentTurretRotation.RotateTurretTowardsTarget(_speed);
        }
        public override void Exit() { /* Логика завершения поворота */ }
    }
}
