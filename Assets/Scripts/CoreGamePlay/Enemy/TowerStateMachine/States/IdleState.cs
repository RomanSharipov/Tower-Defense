using System.Collections.Generic;

namespace Assets.Scripts.CoreGamePlay
{
    public class IdleState : StateBase
    {
        public IdleState(TurretBase tower) : base(tower) { }

        public override void Enter() { /* Логика входа в Idle состояние */ }
        public override void UpdateStateLogic() { /* Логика в Idle состоянии */ }
        public override void Exit() { /* Логика выхода из Idle состояния */ }
    }
}
