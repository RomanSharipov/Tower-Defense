using System;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class DetectorEnyEnemies : IDetector
    {
        private readonly Transform _transform;
        private readonly LayerMask _secondaryTarget;
        private readonly LayerMask _primaryTarget;

        private const int maxColliders = 10;
        private Collider[] _hitColliders = new Collider[maxColliders];
        private float _detectionRadius;
        private DetectorEnemies _detectorEnemies = new DetectorEnemies();
        
        public bool TryFindEnemy(out EnemyBase totalEnemy)
        {
            if (_detectorEnemies.TryFindEnemy(_transform.position, _primaryTarget, _detectionRadius, _hitColliders, out totalEnemy))
            {
                return true;
            }
            return _detectorEnemies.TryFindEnemy(_transform.position, _secondaryTarget, _detectionRadius, _hitColliders, out totalEnemy);
        }

        public DetectorEnyEnemies(Transform transform, LayerMask secondaryTarget, LayerMask primaryTarget)
        {
            _transform = transform;
            _secondaryTarget = secondaryTarget;
            _primaryTarget = primaryTarget;
        }

        public void SetRadius(float detectionRadius)
        {
            _detectionRadius = detectionRadius;
        }

        public bool PointFarAway(Vector3 point)
        {
            return Vector3.Distance(_transform.position, point) > _detectionRadius;
        }
    }
}
