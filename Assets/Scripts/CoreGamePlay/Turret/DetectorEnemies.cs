using System;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class DetectorEnemies 
    {
        private readonly Vector3 _myPosition;

        private const int maxColliders = 10;
        private Collider[] hitColliders = new Collider[maxColliders];
        private float _detectionRadius;
        protected LayerMask _enemyLayerMask = LayerMask.GetMask("Enemy");

        public DetectorEnemies(Vector3 myPosition)
        {
            _myPosition = myPosition;
        }

        public void SetRadius(float detectionRadius)
        {
            _detectionRadius = detectionRadius;
        }

        public bool PointFarAway(Vector3 point)
        {
            Vector3.Distance(_myPosition, point);

            return Vector3.Distance(_myPosition, point) > _detectionRadius;
        }

        public bool TryFindEnemy(out EnemyBase totalEnemy)
        {
            totalEnemy = null;
            
            Array.Clear(hitColliders, 0, hitColliders.Length);

            int numberOfHits = Physics.OverlapSphereNonAlloc(_myPosition, _detectionRadius, hitColliders, _enemyLayerMask);

            EnemyBase closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            for (int i = 0; i < numberOfHits; i++)
            {
                EnemyBase enemy = hitColliders[i].GetComponent<EnemyBase>();
                if (enemy != null)
                {
                    float distanceToEnemy = Vector3.Distance(_myPosition, enemy.transform.position);

                    if (distanceToEnemy < closestDistance)
                    {
                        closestDistance = distanceToEnemy;
                        closestEnemy = enemy;
                    }
                }
            }

            if (closestEnemy != null)
            {
                totalEnemy = closestEnemy;
                return true;
            }
            return false;
        }
    }
}
