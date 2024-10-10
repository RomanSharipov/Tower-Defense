using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class IdleState : StateBase
    {
        public IdleState(TurretBase turret) : base(turret) { }

        public override void Enter() { /* Логика входа в Idle состояние */ }
        public override void UpdateState() {  }
        public override void Exit() { /* Логика выхода из Idle состояния */ }
    }
}
