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
            _antiAirTurretAttack.Init(intervalBetweenAttack: 0.7f, damage: 3);
            _detectorEnyEnemies = new DetectorEnyEnemies(transform.position, _groundEnemy, _flyingEnemy);
            _detectorEnyEnemies.SetRadius(_detectionRadius);
        }
    }
}
