using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class CannonTurret : TurretBase
    {
        [SerializeField] private CannonTurretAttack _cannonTurretAttack;

        private DetectorGroundEnemies _detectorGroundEnemies;
        public override AttackComponent AttackComponent => _cannonTurretAttack;
        
        public override IDetector DetectorEnemies => _detectorGroundEnemies;

        public override TurretId TurretId => TurretId.Cannon;

        public override void InitIntance()
        {
            _detectorGroundEnemies = new DetectorGroundEnemies(transform.position,_groundEnemy);
            
            int indexDamage = _turretsStatsLevelIndexService.GetCurrentValue(StatsType.CannonDamage);
            int indexReloadTime = _turretsStatsLevelIndexService.GetCurrentValue(StatsType.CannonReloadTime);
            int indexAttackDistance = _turretsStatsLevelIndexService.GetCurrentValue(StatsType.CannonAttackDistance);

            int damage = _turretsStatsData.CannonLevelData.Damage[indexDamage].Damage;
            float reloadTime = _turretsStatsData.CannonLevelData.ReloadTime[indexReloadTime].IntervalBeetweenAttack;
            float attackDistance = _turretsStatsData.CannonLevelData.DetectDistance[indexAttackDistance].DetectionRadius;

            _cannonTurretAttack.Init(damage, reloadTime);

            _detectorGroundEnemies.SetRadius(attackDistance);
        }
    }
}
