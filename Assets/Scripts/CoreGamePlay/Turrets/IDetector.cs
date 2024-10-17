using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public interface IDetector
    {
        public bool PointFarAway(Vector3 position);
        public bool TryFindEnemy(out EnemyBase totalEnemy);
    }
}