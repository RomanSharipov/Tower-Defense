using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class SlowTurret : TurretBase
    {
        [SerializeField] private SlowTurretAttack _slowTurretAttack;

        private DetectorEnyEnemies _detectorEnyEnemies;

        public override AttackComponent AttackComponent => _slowTurretAttack;

        public override IDetector DetectorEnemies => _detectorEnyEnemies;

        public override void InitIntance()
        {
            _detectorEnyEnemies = new DetectorEnyEnemies(transform.position,_groundEnemy,_flyingEnemy);
            _detectorEnyEnemies.SetRadius(_detectionRadius);
            _slowTurretAttack.SetConfig(intervalBetweenAttack: 0.7f, damage: 30, bulletSpeed: 5f);
        }
    }
}
