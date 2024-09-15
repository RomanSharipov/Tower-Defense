using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public abstract class TurretBase : MonoBehaviour
    {
        [SerializeField] private ColorTurret _colorTurret;

        private TurretStateMachine _turretStateMachine;
        private bool _enabled;

        public EnemyBase CurrentTarget;

        public TurretStateMachine TurretStateMachine => _turretStateMachine;

        public void SetColor(Color color)
        {
            _colorTurret.SetColor(color);
        }

        public void Init()
        {
            _enabled = true;
            SetColor(Color.DefaultColor);
            ConfigureStateMachine();
        }

        private void Update()
        {
            if (!_enabled)
                return;

            _turretStateMachine.Update();
        }

        private void ConfigureStateMachine()
        {
            _turretStateMachine = new TurretStateMachine();
            ITurretState idleState = new IdleState(this);
            ITurretState rotationToEnemyState = new RotationToEnemyState(this);
            ITurretState attackState = new AttackState(this);
            
            ITurretTransition targetIsNullTransition = new TargetIsNullTransition(this, idleState);
            ITurretTransition enemyNearbyTransition = new EnemyNearbyTransition(this, rotationToEnemyState);
            ITurretTransition rotationToAttackTransition = new RotationToAttackTransition(this, attackState);
            ITurretTransition enemyFarAwayTransition = new EnemyFarAwayTransition(this, idleState);
            
            idleState.AddTransitions(enemyNearbyTransition);
            rotationToEnemyState.AddTransitions(rotationToAttackTransition, targetIsNullTransition, enemyFarAwayTransition);
            attackState.AddTransitions(targetIsNullTransition, enemyFarAwayTransition);

            _turretStateMachine.SetState(idleState);
        }
    }
}
