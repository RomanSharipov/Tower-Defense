using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class CannonTurret : TurretBase
    {
        [SerializeField] private CannonTurretAttack _cannonTurretAttack;

        private DetectorGroundEnemies _detectorGroundEnemies;
        public override AttackComponent AttackComponent => _cannonTurretAttack;

        public override IDetector DetectorEnemies => _detectorGroundEnemies;

        public override void InitIntance()
        {
            _detectorGroundEnemies = new DetectorGroundEnemies(transform.position,_groundEnemy);
            _cannonTurretAttack.Init(intervalBetweenAttack: 0.7f, damage: 30,bulletSpeed: 5f);
            _detectorGroundEnemies.SetRadius(_detectionRadius);
        }
    }
}
