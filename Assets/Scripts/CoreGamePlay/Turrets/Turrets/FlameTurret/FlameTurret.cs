using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class FlameTurret : TurretBase
    {
        [SerializeField] private FlameTurretAttack _flameTurretAttack;

        private DetectorEnyEnemies _detectorEnyEnemies;

        public override AttackComponent AttackComponent => _flameTurretAttack;

        public override IDetector DetectorEnemies => _detectorEnyEnemies;

        public override TurretId TurretId => TurretId.FlameTurret;

        public override void InitIntance()
        {
            int indexDamage = _turretsStatsLevelIndexService.GetCurrentValue(StatsType.FlameDamage);
            int indexAttackDistance = _turretsStatsLevelIndexService.GetCurrentValue(StatsType.FlameAttackDistance);


            int damage = _turretsStatsData.FlameTurretLevelData.Damage[indexDamage].Damage;
            float attackDistance = _turretsStatsData.FlameTurretLevelData.DetectDistance[indexAttackDistance].DetectionRadius;
            
            _detectorEnyEnemies = new DetectorEnyEnemies(transform, _flyingEnemy ,_groundEnemy);
            _detectorEnyEnemies.SetRadius(attackDistance);
            _flameTurretAttack.Init(damage);
        }
    }
}
