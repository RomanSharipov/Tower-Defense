namespace Assets.Scripts.CoreGamePlay
{
    public class RotationToAttackTransition : TransitionBase
    {
        private TurretUpgrade _turretUpgrade;

        public RotationToAttackTransition(TurretBase turret, ITurretState targetState, TurretUpgrade turretUpgrade) : base(turret, targetState)
        {
            _turretUpgrade = turretUpgrade;
        }

        public override bool ShouldTransition()
        {
            return _turretUpgrade.CurrentTurretRotation.IsRotationComplete();
        }
    }
}
