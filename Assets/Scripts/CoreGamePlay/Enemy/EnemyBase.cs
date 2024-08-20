using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField] public Movement _movement; 
        [SerializeField] public TileView _testTile; 
        
        public void Init(TileData[] pathPoints, string name)
        {
            _movement.NewMovement(transform, name);
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
