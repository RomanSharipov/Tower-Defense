using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class AttackState : StateBase
    {
        private readonly TurretView _turretView;
        private float _speed = 200.0f;

        public AttackState(TurretBase turret, TurretView turretView) : base(turret) 
        {
            _turretView = turretView;
        }

        public override void Enter() 
        { 
            _turret.AttackComponent.OnStartAttack(_turret.CurrentTarget); 
        }

        public override void UpdateState() 
        {
            _turretView.CurrentTurretRotation.RotateTurretTowardsTarget(_speed);
            _turret.AttackComponent.AttackIfNeeded();
        }
        public override void Exit() 
        {
            _turret.AttackComponent.OnEndAttack();
        }
    }
}
