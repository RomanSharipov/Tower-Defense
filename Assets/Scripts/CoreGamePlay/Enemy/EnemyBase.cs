using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public abstract class EnemyBase : MonoBehaviour
    {
        private Movement _movement; 
        
        public void Init(TileData[] pathPoints)
        {
            _movement = new Movement(transform);
            _movement.SetPath(pathPoints);
            _movement.StartMovement();
        }
        public void UpdatePathIfNeeded(TileData newUnwalkableTile)
        {
            _movement.UpdatePathIfNeeded(newUnwalkableTile);
        }

        private void OnDestroy()
        {
            _movement.StopMovement();
        }

    }
}
