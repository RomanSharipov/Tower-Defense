using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class FlameTurret : TurretBase
    {
        [SerializeField] private FlameTurretAttack _flameTurretAttack;

        private DetectorEnyEnemies _detectorEnyEnemies;

        public override AttackComponent AttackComponent => _flameTurretAttack;

        public override IDetector DetectorEnemies => _detectorEnyEnemies;

        public override void InitIntance()
        {
            _detectorEnyEnemies = new DetectorEnyEnemies(transform.position, _flyingEnemy ,_groundEnemy);
            _detectorEnyEnemies.SetRadius(_detectionRadius);
            _flameTurretAttack.Init(damage: 1);
        }
    }
}
