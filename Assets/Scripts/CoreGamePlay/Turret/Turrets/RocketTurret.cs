using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class RocketTurret : TurretBase
    {
        [SerializeField] private RocketTurretAttack _rocketTurretAttack;

        public override AttackComponent AttackComponent => _rocketTurretAttack;

        public override void InitAttackComponent()
        {
            _rocketTurretAttack.SetConfig(intervalBetweenAttack: 1.7f, damage: 30, bulletSpeed: 5f);
        }
    }
}
