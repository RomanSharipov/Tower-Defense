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
            int damage = _turretsStatsData.MinigunLevelData.DamageUpgrade[0].Damage;
            float attackDistance = _turretsStatsData.MinigunLevelData.DetectDistance[0].DetectionRadius;
            
            _detectorEnyEnemies = new DetectorEnyEnemies(transform.position, _flyingEnemy, _groundEnemy);
            _detectorEnyEnemies.SetRadius(attackDistance);
            _minigunAttack.Init(damage);
        }
    }
}
