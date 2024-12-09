using CodeBase.Infrastructure.Services;
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
            int indexDamage = _turretsStatsLevelIndexService.GetCurrentValue(StatsType.SlowDamage);
            int indexSlowPercent = _turretsStatsLevelIndexService.GetCurrentValue(StatsType.SlowPercent);
            int indexSlowDuration = _turretsStatsLevelIndexService.GetCurrentValue(StatsType.SlowDuration);
            int indexAttackDistance = _turretsStatsLevelIndexService.GetCurrentValue(StatsType.SlowAttackDistance);


            int damage = _turretsStatsData.SlowTurretData.DamageUpgrade[indexDamage].Damage;
            int percentSlow = _turretsStatsData.SlowTurretData.SlowPercentUpgrade[indexSlowPercent].Percent;
            float durationSlow = _turretsStatsData.SlowTurretData.SlowDurationUpgrade[indexSlowDuration].Duration;
            float attackDistance = _turretsStatsData.SlowTurretData.DetectDistance[indexAttackDistance].DetectionRadius;


            _detectorEnyEnemies = new DetectorEnyEnemies(transform.position, _flyingEnemy ,_groundEnemy);
            _detectorEnyEnemies.SetRadius(attackDistance);
            //_slowTurretAttack.Init(damage: 1,percent:70,duration:3.0f);
            _slowTurretAttack.Init(damage: damage, percent: percentSlow, duration: durationSlow);
        }
    }
}
