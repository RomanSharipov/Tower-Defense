using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public abstract class TurretBase : MonoBehaviour
    {
        [SerializeField] private ColorTurret _colorTurret;
        [SerializeField] private TurretRotation _turretRotation;

        private TurretStateMachine _turretStateMachine;
        private DetectorEnemies _detectorEnemies;
        private bool _enabled;

        public EnemyBase CurrentTarget;

        //for config
        private float _detectionRadius = 5.0f;

        public TurretStateMachine TurretStateMachine => _turretStateMachine;
        public DetectorEnemies DetectorEnemies => _detectorEnemies;

        public void SetColor(Color color)
        {
            _colorTurret.SetColor(color);
        }

        public void Init()
        {
            _detectorEnemies = new DetectorEnemies(this);
            _detectorEnemies.SetRadius(_detectionRadius);
            SetColor(Color.DefaultColor);
            ConfigureStateMachine();

            _enabled = true;
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
            ITurretState rotationToEnemyState = new RotationToEnemyState(this, _turretRotation);
            ITurretState attackState = new AttackState(this, _turretRotation);
            
            ITurretTransition targetIsNullTransition = new TargetIsNullTransition(this, idleState);
            ITurretTransition enemyNearbyTransition = new EnemyNearbyTransition(this, rotationToEnemyState);
            ITurretTransition rotationToAttackTransition = new RotationToAttackTransition(this, attackState, _turretRotation);
            ITurretTransition enemyFarAwayTransition = new EnemyFarAwayTransition(this, idleState);
            
            idleState.AddTransitions(enemyNearbyTransition);
            rotationToEnemyState.AddTransitions(rotationToAttackTransition, targetIsNullTransition, enemyFarAwayTransition);
            attackState.AddTransitions(targetIsNullTransition, enemyFarAwayTransition);
            
            _turretStateMachine.SetState(idleState);
        }
    }
}
