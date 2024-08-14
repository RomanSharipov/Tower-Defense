using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public abstract class EnemyBase : MonoBehaviour
    {
        private Movement _movement; 
        
        public void Init(Vector3[] pathPoints)
        {
            _movement = new Movement(transform);
            _movement.SetPath(pathPoints);
            _movement.StartMovement();
        }
    }
}
