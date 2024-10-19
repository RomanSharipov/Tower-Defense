using System;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class DetectorEnemies
    {
        public bool PointFarAway(Vector3 myPosition,Vector3 point, float detectionRadius)
        {
            return Vector3.Distance(myPosition, point) > detectionRadius;
        }

        public bool TryFindEnemy(Vector3 myPosition, LayerMask enemyLayerMask, float detectionRadius, Collider[] hitColliders, out EnemyBase totalEnemy)
        {
            totalEnemy = null;

            Array.Clear(hitColliders, 0, hitColliders.Length);

            int numberOfHits = Physics.OverlapSphereNonAlloc(myPosition, detectionRadius, hitColliders, enemyLayerMask);

            EnemyBase closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            for (int i = 0; i < numberOfHits; i++)
            {
                EnemyBase enemy = hitColliders[i].GetComponent<EnemyBase>();
                if (enemy != null)
                {
                    float distanceToEnemy = Vector3.Distance(myPosition, enemy.transform.position);

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
                return !PointFarAway(myPosition, closestEnemy.Position, detectionRadius);
            }
            return false;
        }
    }
}
