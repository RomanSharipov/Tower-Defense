using System;
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
        private TileView _tileView;
        
        private bool _enabled;
        private Action _setIdleState;

        public EnemyBase CurrentTarget;
        public TileView TileView => _tileView;
        
        [Inject] protected ITurretsStatsData _turretsStatsData;
        [Inject] protected ITurretsStatsLevelIndexService _turretsStatsLevelIndexService;
        [Inject] protected ITurretRemover _turretRemover;
        public TurretStateMachine TurretStateMachine => _turretStateMachine;
        public abstract IDetector DetectorEnemies { get; }
        public abstract AttackComponent AttackComponent { get; }
        public abstract void InitIntance();

        public void SetColor(ColorType color)
        {
            _colorTurret.SetColor(color);
        }

        public void RemoveSelf()
        {
            _turretRemover.TurretRemovedInvoke(this);
            Destroy(gameObject);
        }
        
        public void Init(TileView tileView)
        {
            _tileView = tileView;
            ConfigureStateMachine();
            ConfigureTurretUpgrade();
            SetColor(ColorType.DefaultColor);
            InitIntance();
            _enabled = true;
        }
        
        private void ConfigureTurretUpgrade()
        {
            _turretUpgrade = new TurretUpgrade(maxLevel:3);
            _turretUpgrade.RegisterUpgradeable(AttackComponent);
            _turretUpgrade.RegisterUpgradeable(_turretView);
            _turretUpgrade.ResetLevel();
            _turretUpgrade.TurretUpgraded += _setIdleState;
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

            _setIdleState = () => _turretStateMachine.SetState(idleState);
            _setIdleState();
        }


        private void OnDestroy()
        {
            if (!_enabled)
                return;

            _turretUpgrade.TurretUpgraded -= _setIdleState;
        }
    }
}
