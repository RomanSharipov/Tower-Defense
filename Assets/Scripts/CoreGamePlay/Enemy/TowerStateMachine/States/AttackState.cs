using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class AttackState : StateBase
    {
        public AttackState(TurretBase turret) : base(turret) { }

        public override void Enter() { }
        public override void UpdateState() { Debug.Log($"AttackState Update"); }
        public override void Exit() { }
    }
}
