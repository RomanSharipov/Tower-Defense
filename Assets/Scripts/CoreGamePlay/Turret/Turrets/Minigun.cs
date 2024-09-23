using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class Minigun : TurretBase
    {
        [SerializeField] private MinigunAttack _minigunAttack;

        public override IAttackComponent AttackComponent => _minigunAttack;
    }
}
