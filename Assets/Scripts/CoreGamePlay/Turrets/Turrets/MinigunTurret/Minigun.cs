using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class Minigun : TurretBase
    {
        [SerializeField] private MinigunAttack _minigunAttack;

        private DetectorEnyEnemies _detectorEnyEnemies;

        public override AttackComponent AttackComponent => _minigunAttack;

        public override IDetector DetectorEnemies => _detectorEnyEnemies;

        public override void InitIntance()
        {
            _detectorEnyEnemies = new DetectorEnyEnemies(transform.position, _groundEnemy, _flyingEnemy);
            _detectorEnyEnemies.SetRadius(_detectionRadius);
            _minigunAttack.SetConfig(intervalBetweenAttack:0.1f, damage:2,bulletSpeed:0);
        }
    }
}
