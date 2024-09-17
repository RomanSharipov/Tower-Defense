using System;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class EnemyNearbyTransition : TransitionBase
    {
        private const int maxColliders = 10; 
        private Collider[] hitColliders = new Collider[maxColliders]; 
        private float detectionRadius = 10f; 
        
        public EnemyNearbyTransition(TurretBase turret, ITurretState targetState) : base(turret, targetState)
        {
            
        }

        public override bool ShouldTransition()
        {
            return TryFindEnemy();
        }

        private bool TryFindEnemy()
        {
            Vector3 turretPosition = _turret.transform.position;

            Array.Clear(hitColliders, 0, hitColliders.Length);

            int numberOfHits = Physics.OverlapSphereNonAlloc(turretPosition, detectionRadius, hitColliders, _enemyLayerMask);

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
