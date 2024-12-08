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
            int damage = _turretsStatsData.AntiAirLevelData.DamageUpgrade[0].Damage;
            float reloadTime = _turretsStatsData.AntiAirLevelData.ReloadTimeUpgrade[0].IntervalBeetweenAttack;
            float attackDistance = _turretsStatsData.AntiAirLevelData.DetectDistance[0].DetectionRadius;


            _antiAirTurretAttack.Init(reloadTime, damage);
            _detectorEnyEnemies = new DetectorEnyEnemies(transform.position, _groundEnemy, _flyingEnemy);
            _detectorEnyEnemies.SetRadius(attackDistance);
        }
    }
}
