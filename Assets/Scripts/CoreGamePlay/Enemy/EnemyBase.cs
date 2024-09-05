using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField] public EnemyMovement _movement; 
        [SerializeField] public TileView _testTile; 
        
        public EnemyMovement Movement => _movement;

        public void Init(List<TileData> pathPoints)
        {
            _movement.SetPath(pathPoints);
            _movement.SetCurrentTarget(0);
            _movement.StartMovement();
        }
        
        private void OnDestroy()
        {
            _movement.StopMovement();
        }
    }
}
