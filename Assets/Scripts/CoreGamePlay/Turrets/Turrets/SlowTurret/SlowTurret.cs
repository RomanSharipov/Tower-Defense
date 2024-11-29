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
            int damage = _turretsStatsProvider.SlowTurretData.DamageUpgrade[0].Damage;
            int percentSlow = _turretsStatsProvider.SlowTurretData.SlowPercentUpgrade[0].Percent;
            float durationSlow = _turretsStatsProvider.SlowTurretData.SlowDurationUpgrade[0].Duration;
            float attackDistance = _turretsStatsProvider.SlowTurretData.DetectDistance[0].DetectionRadius;


            _detectorEnyEnemies = new DetectorEnyEnemies(transform.position, _flyingEnemy ,_groundEnemy);
            _detectorEnyEnemies.SetRadius(attackDistance);
            //_slowTurretAttack.Init(damage: 1,percent:70,duration:3.0f);
            _slowTurretAttack.Init(damage: damage, percent: percentSlow, duration: durationSlow);
        }
    }
}
