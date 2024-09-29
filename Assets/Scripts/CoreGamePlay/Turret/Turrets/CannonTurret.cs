using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class CannonTurret : TurretBase
    {
        [SerializeField] private CannonTurretAttack _cannonTurretAttack;

        public override AttackComponent AttackComponent => _cannonTurretAttack;

        public override void InitAttackComponent()
        {
            _cannonTurretAttack.SetConfig(intervalBetweenAttack: 2.7f, damage: 10,bulletSpeed: 20f);
        }
    }
}
