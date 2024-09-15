using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public abstract class TurretBase : MonoBehaviour
    {
        [SerializeField] private ColorTurret _colorTurret;

        private TurretStateMachine _turretStateMachine;

        public EnemyBase CurrentTarget;

        public TurretStateMachine TurretStateMachine => _turretStateMachine;

        public void SetColor(Color color)
        {
            _colorTurret.SetColor(color);
        }

        private void ConfigureStateMachine()
        {
            ITurretState idleState = new IdleState(this);
            ITurretState rotationToEnemyState = new RotationToEnemyState(this);
            ITurretState attackState = new AttackState(this);
            
            ITransition targetIsNullTransition = new TargetIsNullTransition(this, idleState);
            ITransition enemyNearbyTransition = new EnemyNearbyTransition(this, rotationToEnemyState);
            ITransition rotationToAttackTransition = new RotationToAttackTransition(this, attackState);
            ITransition enemyFarAwayTransition = new EnemyFarAwayTransition(this, idleState);


            idleState.AddTransitions(enemyNearbyTransition);
            rotationToEnemyState.AddTransitions(rotationToAttackTransition, targetIsNullTransition, enemyFarAwayTransition);
            attackState.AddTransitions(targetIsNullTransition, enemyFarAwayTransition);

            TurretStateMachine.SetState(idleState);
        }
    }
}
