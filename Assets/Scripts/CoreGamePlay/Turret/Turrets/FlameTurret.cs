using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class FlameTurret : TurretBase
    {
        [SerializeField] private FlameTurretAttack _flameTurretAttack;

        public override IAttackComponent AttackComponent => _flameTurretAttack;
    }
}
