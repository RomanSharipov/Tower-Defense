using System;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class DetectorEnyEnemies : IDetector
    {
        private readonly Vector3 _myPosition;
        private readonly LayerMask _secondaryTarget;
        private readonly LayerMask _primaryTarget;

        private const int maxColliders = 10;
        private Collider[] _hitColliders = new Collider[maxColliders];
        private float _detectionRadius;
        private DetectorEnemies _detectorEnemies = new DetectorEnemies();
        
        public bool TryFindEnemy(out EnemyBase totalEnemy)
        {
            if (_detectorEnemies.TryFindEnemy(_myPosition, _primaryTarget, _detectionRadius, _hitColliders, out totalEnemy))
            {
                return true;
            }
            return _detectorEnemies.TryFindEnemy(_myPosition, _secondaryTarget, _detectionRadius, _hitColliders, out totalEnemy);
        }

        public DetectorEnyEnemies(Vector3 myPosition, LayerMask secondaryTarget, LayerMask primaryTarget)
        {
            _myPosition = myPosition;
            _secondaryTarget = secondaryTarget;
            _primaryTarget = primaryTarget;
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
