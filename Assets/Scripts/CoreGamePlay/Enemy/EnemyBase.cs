using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField] public EnemyMovement _movement; 
        [SerializeField] public TileView _testTile; 
        
        public EnemyMovement Movement => _movement;

        public void Init(TileData[] pathPoints, string name)
        {
            _movement.NewMovement(transform, name);
            _movement.SetPath(pathPoints);
            _movement.StartMovement();
        }
        
        private void OnDestroy()
        {
            _movement.StopMovement();
        }
    }
}
