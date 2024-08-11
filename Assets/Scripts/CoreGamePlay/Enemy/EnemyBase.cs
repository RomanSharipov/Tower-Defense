using UnityEngine;

namespace Assets.Scripts.CoreGamePlay.Enemy
{
    public abstract class EnemyBase : MonoBehaviour
    {
        private Movement _movement; 
        
        public void Init(Transform[] pathPoints)
        {
            _movement = new Movement(transform);
            _movement.SetPath(pathPoints);
            _movement.StartMovement();
        }
    }
}
