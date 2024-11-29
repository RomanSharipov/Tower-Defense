using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class CannonTurret : TurretBase
    {
        [SerializeField] private CannonTurretAttack _cannonTurretAttack;

        private DetectorGroundEnemies _detectorGroundEnemies;
        public override AttackComponent AttackComponent => _cannonTurretAttack;
        
        public override IDetector DetectorEnemies => _detectorGroundEnemies;

        public override void InitIntance()
        {
            _detectorGroundEnemies = new DetectorGroundEnemies(transform.position,_groundEnemy);

            int damage = _turretsStatsProvider.CannonLevelData.DamageUpgrade[0].Damage;
            float reloadTime = _turretsStatsProvider.CannonLevelData.ReloadTimeUpgrade[0].IntervalBeetweenAttack;
            float attackDistance = _turretsStatsProvider.CannonLevelData.DetectDistance[0].DetectionRadius;

            _cannonTurretAttack.Init(damage, reloadTime);

            _detectorGroundEnemies.SetRadius(attackDistance);
        }
    }
}
