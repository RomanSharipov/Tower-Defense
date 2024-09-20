using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class AttackState : StateBase
    {
        private readonly TurretRotation _turretRotation;
        private float _speed = 200.0f;

        public AttackState(TurretBase turret, TurretRotation turretRotation) : base(turret) 
        {
            _turretRotation = turretRotation;
        }

        public override void Enter() { }
        public override void UpdateState() 
        {
            _turretRotation.RotateTurretTowardsTarget(_speed);
        }
        public override void Exit() { }
    }
}
