using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public abstract class TurretBase : MonoBehaviour
    {
        [SerializeField] private ColorTurret _colorTurret;
        [SerializeField] protected TurretUpgrade _turretUpgrade;
        [SerializeField] private TurretView _turretView;
        [SerializeField] private LayerMask _enemyLayerMask;

        private TurretStateMachine _turretStateMachine;
        private DetectorEnemies _detectorEnemies;
        private bool _enabled;

        public EnemyBase CurrentTarget;

        //for config
        private float _detectionRadius = 5.0f;

        public TurretStateMachine TurretStateMachine => _turretStateMachine;
        public DetectorEnemies DetectorEnemies => _detectorEnemies;
        public abstract AttackComponent AttackComponent { get; }
        public abstract void InitAttackComponent();

        public void SetColor(ColorType color)
        {
            _colorTurret.SetColor(color);
        }

        public void Init()
        {
            ConfigureDetectorEnemies();
            ConfigureStateMachine();
            ConfigureTurretUpgrade();
            SetColor(ColorType.DefaultColor);
            InitAttackComponent();
            _enabled = true;
        }

        private void ConfigureDetectorEnemies()
        {
            _detectorEnemies = new DetectorEnemies(transform.position, _enemyLayerMask);
            _detectorEnemies.SetRadius(_detectionRadius);
        }

        private void ConfigureTurretUpgrade()
        {
            _turretUpgrade = new TurretUpgrade(maxLevel:3);
            _turretUpgrade.RegisterUpgradeable(AttackComponent);
            _turretUpgrade.RegisterUpgradeable(_turretView);
            _turretUpgrade.ResetLevel();
        }

        [ContextMenu("LevelUpTest()")]
        public void LevelUpTest()
        {
            _turretUpgrade.LevelUp();
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
            ITurretState rotationToEnemyState = new RotationToEnemyState(this, _turretView);
            ITurretState attackState = new AttackState(this, _turretView);
            
            ITurretTransition targetIsNullTransition = new TargetIsNullTransition(this, idleState);
            ITurretTransition enemyNearbyTransition = new EnemyNearbyTransition(this, rotationToEnemyState);
            ITurretTransition rotationToAttackTransition = new RotationToAttackTransition(this, attackState, _turretView);
            ITurretTransition enemyFarAwayTransition = new EnemyFarAwayTransition(this, idleState);
            
            idleState.AddTransitions(enemyNearbyTransition);
            rotationToEnemyState.AddTransitions(rotationToAttackTransition, targetIsNullTransition, enemyFarAwayTransition);
            attackState.AddTransitions(targetIsNullTransition, enemyFarAwayTransition);
            
            _turretStateMachine.SetState(idleState);
        }
    }
}
