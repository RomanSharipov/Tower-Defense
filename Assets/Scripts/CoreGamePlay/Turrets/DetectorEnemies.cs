using System;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class DetectorEnemies 
    {
        private readonly TurretBase _turret;

        private const int maxColliders = 10;
        private Collider[] hitColliders = new Collider[maxColliders];
        private float _detectionRadius;
        protected LayerMask _enemyLayerMask = LayerMask.GetMask("Enemy");

        public DetectorEnemies(TurretBase turret)
        {
            _turret = turret;
        }

        public void SetRadius(float detectionRadius)
        {
            _detectionRadius = detectionRadius;
        }

        public bool EnemyFarAway()
        {
            if (_turret.CurrentTarget == null)
                return true;

            Vector3.Distance(_turret.transform.position, _turret.CurrentTarget.Position);

            return Vector3.Distance(_turret.transform.position, _turret.CurrentTarget.Position) > _detectionRadius;
        }

        public bool TryFindEnemy()
        {
            Vector3 turretPosition = _turret.transform.position;

            Array.Clear(hitColliders, 0, hitColliders.Length);

            int numberOfHits = Physics.OverlapSphereNonAlloc(turretPosition, _detectionRadius, hitColliders, _enemyLayerMask);

            EnemyBase closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            for (int i = 0; i < numberOfHits; i++)
            {
                EnemyBase enemy = hitColliders[i].GetComponent<EnemyBase>();
                if (enemy != null)
                {
                    float distanceToEnemy = Vector3.Distance(turretPosition, enemy.transform.position);

                    if (distanceToEnemy < closestDistance)
                    {
                        closestDistance = distanceToEnemy;
                        closestEnemy = enemy;
                    }
                }
            }

            if (closestEnemy != null)
            {
                _turret.CurrentTarget = closestEnemy;
                return true;
            }

            _turret.CurrentTarget = null;
            return false;
        }
    }
}
