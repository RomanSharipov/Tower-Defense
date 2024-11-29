using CodeBase.Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.CoreGamePlay
{
    public abstract class TurretBase : MonoBehaviour
    {
        [SerializeField] private ColorTurret _colorTurret;
        [SerializeField] protected TurretUpgrade _turretUpgrade;
        [SerializeField] private TurretView _turretView;

        [SerializeField] protected LayerMask _groundEnemy;
        [SerializeField] protected LayerMask _flyingEnemy;

        private TurretStateMachine _turretStateMachine;
        private DetectorGroundEnemies _detectorEnemies;
        private bool _enabled;

        public EnemyBase CurrentTarget;

        //for config
        [SerializeField] protected float _detectionRadius = 5.0f;
        [Inject] protected ITurretsStatsProvider _turretsStatsProvider;
        public TurretStateMachine TurretStateMachine => _turretStateMachine;
        public abstract IDetector DetectorEnemies { get; }
        public abstract AttackComponent AttackComponent { get; }
        public abstract void InitIntance();

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
            InitIntance();
            _enabled = true;
        }

        private void ConfigureDetectorEnemies()
        {
            _detectorEnemies = new DetectorGroundEnemies(transform.position, _groundEnemy);
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
