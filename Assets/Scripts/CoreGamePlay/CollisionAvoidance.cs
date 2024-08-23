using UnityEngine;

public class CollisionAvoidance : MonoBehaviour
{
    public LayerMask enemyLayer;
    public float detectionDistance = 1.0f;
    public float scale = 1.0f;
    
    private EnemyMovement myMovement;
    

    private void Awake()
    {
        myMovement = GetComponent<EnemyMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.TryGetComponent(out Movement otherMovement)) 
        //{
        //    myMovement.StopMovement();
        //    otherMovement.StopMovement();
        //}
    }
}
