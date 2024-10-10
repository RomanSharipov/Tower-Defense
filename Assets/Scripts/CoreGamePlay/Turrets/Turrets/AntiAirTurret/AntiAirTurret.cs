using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class AntiAirTurret : TurretBase
    {
        [SerializeField] private AntiAirTurretAttack _antiAirTurretAttack;

        public override AttackComponent AttackComponent => _antiAirTurretAttack;

        public override void InitAttackComponent()
        {
            _antiAirTurretAttack.SetConfig(intervalBetweenAttack: 0.7f, damage: 30,bulletSpeed: 5f);
        }
    }
}
