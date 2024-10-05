using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class SlowTurret : TurretBase
    {
        [SerializeField] private SlowTurretAttack _slowTurretAttack;

        public override AttackComponent AttackComponent => _slowTurretAttack;

        public override void InitAttackComponent()
        {
            _slowTurretAttack.SetConfig(intervalBetweenAttack: 0.7f, damage: 30, bulletSpeed: 5f);
        }
    }
}
