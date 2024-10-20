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
            _detectorEnyEnemies = new DetectorEnyEnemies(transform.position, _flyingEnemy ,_groundEnemy);
            _detectorEnyEnemies.SetRadius(_detectionRadius);
            _slowTurretAttack.SetConfig(intervalBetweenAttack:0, damage: 5, 0);
        }
    }
}
