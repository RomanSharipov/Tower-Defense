using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class FlameTurret : TurretBase
    {
        [SerializeField] private FlameTurretAttack _flameTurretAttack;

        public override AttackComponent AttackComponent => _flameTurretAttack;

        public override void InitAttackComponent()
        {
            _flameTurretAttack.SetConfig(intervalBetweenAttack:0, damage: 1, 0);
        }
    }
}
