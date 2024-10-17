using System;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class DetectorGroundEnemies : IDetector
    {
        private readonly Vector3 _myPosition;
        private readonly LayerMask _enemyLayerMask;

        private const int maxColliders = 10;
        private Collider[] _hitColliders = new Collider[maxColliders];
        private float _detectionRadius;
        private DetectorEnemies _detectorEnemies = new DetectorEnemies();
        
        public bool TryFindEnemy(out EnemyBase totalEnemy)
        {
            return _detectorEnemies.TryFindEnemy(_myPosition, _enemyLayerMask, _detectionRadius, _hitColliders, out totalEnemy);
        }

        public DetectorGroundEnemies(Vector3 myPosition, LayerMask enemyLayerMask)
        {
            _myPosition = myPosition;
            _enemyLayerMask = enemyLayerMask;
        }

        public void SetRadius(float detectionRadius)
        {
            _detectionRadius = detectionRadius;
        }

        public bool PointFarAway(Vector3 point)
        {
            return Vector3.Distance(_myPosition, point) > _detectionRadius;
        }
    }
}
