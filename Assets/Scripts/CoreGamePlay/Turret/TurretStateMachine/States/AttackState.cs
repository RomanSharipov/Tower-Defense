using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class AttackState : StateBase
    {
        private readonly TurretUpgrade _turretUpgrade;
        private float _speed = 200.0f;

        public AttackState(TurretBase turret, TurretUpgrade turretUpgrade) : base(turret) 
        {
            _turretUpgrade = turretUpgrade;
        }

        public override void Enter() { }
        public override void UpdateState() 
        {
            _turretUpgrade.CurrentUpgradeTurretRotation.RotateTurretTowardsTarget(_speed);
            _turret.AttackComponent.TryAttack(_turret.CurrentTarget);
        }
        public override void Exit() { }
    }
}
