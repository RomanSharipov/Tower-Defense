using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class RotationToEnemyState : StateBase
    {
        public RotationToEnemyState(TurretBase turret) : base(turret) { }

        public override void Enter() { /* Логика начала поворота */ }
        public override void UpdateState() { Debug.Log($"RotationToEnemyState Update"); }
        public override void Exit() { /* Логика завершения поворота */ }
    }
}
