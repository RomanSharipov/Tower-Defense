using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;

namespace Assets.Scripts.CoreGamePlay
{
    public class RotationToEnemyState : StateBase
    {
        public RotationToEnemyState(TurretBase turret) : base(turret) { }

        public override void Enter() { /* Логика начала поворота */ }
        public override void UpdateStateLogic() { }
        public override void Exit() { /* Логика завершения поворота */ }
    }
}
