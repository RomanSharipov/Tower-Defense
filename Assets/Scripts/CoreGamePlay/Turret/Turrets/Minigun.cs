using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class Minigun : TurretBase
    {
        [SerializeField] private MinigunAttack _minigunAttack;

        public override IAttackComponent AttackComponent => _minigunAttack;

        public override void InitAttackComponent()
        {
            _minigunAttack.SetConfig(intervalBetweenAttack:0.1f, damage:2);
        }
    }
}
