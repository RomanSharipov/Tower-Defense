namespace Assets.Scripts.CoreGamePlay
{
    public class RotationToAttackTransition : TransitionBase
    {
        private TurretView _turretView;

        public RotationToAttackTransition(TurretBase turret, ITurretState targetState, TurretView turretView) : base(turret, targetState)
        {
            _turretView = turretView;
        }

        public override bool ShouldTransition()
        {
            return _turretView.CurrentTurretRotation.IsRotationComplete();
        }
    }
}
