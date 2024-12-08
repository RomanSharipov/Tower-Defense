using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class RocketTurret : TurretBase
    {
        [SerializeField] private RocketTurretAttack _rocketTurretAttack;

        private DetectorGroundEnemies _detectorGroundEnemies;

        public override AttackComponent AttackComponent => _rocketTurretAttack;

        public override IDetector DetectorEnemies => _detectorGroundEnemies;

        public override void InitIntance()
        {
            int damage = _turretsStatsData.RocketTurretLevelData.DamageUpgrade[0].Damage;
            float reloadTime = _turretsStatsData.RocketTurretLevelData.ReloadTimeUpgrade[0].IntervalBeetweenAttack;
            float attackDistance = _turretsStatsData.RocketTurretLevelData.DetectDistance[0].DetectionRadius;
            
            _detectorGroundEnemies = new DetectorGroundEnemies(transform.position, _groundEnemy);
            _detectorGroundEnemies.SetRadius(attackDistance);
            _rocketTurretAttack.Init(reloadTime,damage);
        }
    }
}
