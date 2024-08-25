using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField] public EnemyMovement _movement; 
        [SerializeField] public TileView _testTile; 
        
        public EnemyMovement Movement => _movement;

        public void Init(TileData[] pathPoints, ICacherOfPath cacherOfPath,EnemySpawner enemySpawner)
        {
            _movement.Construct(cacherOfPath, enemySpawner);
            _movement.SetPath(pathPoints);
            _movement.StartMovement();
        }
        
        private void OnDestroy()
        {
            _movement.StopMovement();
        }
    }
}
