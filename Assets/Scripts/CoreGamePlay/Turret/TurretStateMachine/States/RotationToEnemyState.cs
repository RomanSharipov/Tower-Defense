using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class RotationToEnemyState : StateBase
    {
        private readonly TurretView _turretView;
        private float _speed = 9.0f;

        public RotationToEnemyState(TurretBase turret, TurretView turretView) : base(turret)
        {
            _turretView = turretView;
        }
        public override void UpdateState() 
        {
            _turretView.CurrentTurretRotation.RotateTurretTowardsTarget(_speed);
        }

        public override void Enter() {  }

        public override void Exit() {  }
    }
}
