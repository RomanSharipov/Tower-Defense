using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "StaticData/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private float _movementSpeed;

        public float MovementSpeed => _movementSpeed;
    }
}
