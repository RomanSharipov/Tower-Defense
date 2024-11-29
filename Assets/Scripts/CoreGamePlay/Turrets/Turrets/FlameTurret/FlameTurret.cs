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
            int damage = _turretsStatsProvider.FlameTurretLevelData.DamageUpgrade[0].Damage;
            float attackDistance = _turretsStatsProvider.FlameTurretLevelData.DetectDistance[0].DetectionRadius;
            
            _detectorEnyEnemies = new DetectorEnyEnemies(transform.position, _flyingEnemy ,_groundEnemy);
            _detectorEnyEnemies.SetRadius(attackDistance);
            _flameTurretAttack.Init(damage);
        }
    }
}
