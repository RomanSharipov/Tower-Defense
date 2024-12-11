using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class AntiAirTurret : TurretBase
    {
        [SerializeField] private AntiAirTurretAttack _antiAirTurretAttack;
        
        private DetectorEnyEnemies _detectorEnyEnemies;
        public override AttackComponent AttackComponent => _antiAirTurretAttack;

        public override IDetector DetectorEnemies => _detectorEnyEnemies;

        public override void InitIntance()
        {
            int indexDamage = _turretsStatsLevelIndexService.GetCurrentValue(StatsType.AntiAirDamage);
            int indexReloadTime = _turretsStatsLevelIndexService.GetCurrentValue(StatsType.AntiAirReloadTime);
            int indexAttackDistance = _turretsStatsLevelIndexService.GetCurrentValue(StatsType.AntiAirAttackDistance);
            
            int damage = _turretsStatsData.AntiAirLevelData.Damage[indexDamage].Damage;
            float reloadTime = _turretsStatsData.AntiAirLevelData.ReloadTime[indexReloadTime].IntervalBeetweenAttack;
            float attackDistance = _turretsStatsData.AntiAirLevelData.DetectDistance[indexAttackDistance].DetectionRadius;
            
            _antiAirTurretAttack.Init(reloadTime, damage);
            _detectorEnyEnemies = new DetectorEnyEnemies(transform, _groundEnemy, _flyingEnemy);
            _detectorEnyEnemies.SetRadius(attackDistance);
        }
    }
}
