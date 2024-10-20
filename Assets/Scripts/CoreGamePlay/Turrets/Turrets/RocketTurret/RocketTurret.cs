using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class RocketTurret : TurretBase
    {
        [SerializeField] private RocketTurretAttack _rocketTurretAttack;

        private DetectorGroundEnemies _detectorGroundEnemies;

        public override AttackComponent AttackComponent => _rocketTurretAttack;

        public override IDetector DetectorEnemies => _detectorGroundEnemies;

        public override void InitIntance()
        {
            _detectorGroundEnemies = new DetectorGroundEnemies(transform.position, _groundEnemy);
            _detectorGroundEnemies.SetRadius(_detectionRadius);
            _rocketTurretAttack.Init(intervalBetweenAttack: 1.7f, damage: 30, bulletSpeed: 5f);
        }
    }
}
