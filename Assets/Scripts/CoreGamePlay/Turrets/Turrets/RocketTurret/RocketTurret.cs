using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class RocketTurret : TurretBase
    {
        [SerializeField] private RocketTurretAttack _rocketTurretAttack;

        private DetectorGroundEnemies _detectorGroundEnemies;

        public override AttackComponent AttackComponent => _rocketTurretAttack;

        public override IDetector DetectorEnemies => _detectorGroundEnemies;

        public override TurretId TurretId => TurretId.Rocket;

        public override void InitIntance()
        {
            int indexDamage = _turretsStatsLevelIndexService.GetCurrentValue(StatsType.RocketDamage);
            int indexReloadTime = _turretsStatsLevelIndexService.GetCurrentValue(StatsType.RocketReloadTime);
            int indexAttackDistance = _turretsStatsLevelIndexService.GetCurrentValue(StatsType.RocketAttackDistance);
            
            int damage = _turretsStatsData.RocketTurretLevelData.Damage[indexDamage].Damage;
            float reloadTime = _turretsStatsData.RocketTurretLevelData.ReloadTime[indexReloadTime].IntervalBeetweenAttack;
            float attackDistance = _turretsStatsData.RocketTurretLevelData.DetectDistance[indexAttackDistance].DetectionRadius;
            
            _detectorGroundEnemies = new DetectorGroundEnemies(transform.position, _groundEnemy);
            _detectorGroundEnemies.SetRadius(attackDistance);
            _rocketTurretAttack.Init(reloadTime,damage);
        }
    }
}
