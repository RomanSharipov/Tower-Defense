using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(fileName = "SubWaveData", menuName = "StaticData/SubWaveData")]
    public class SubWaveData : ScriptableObject
    {
        [SerializeField] private int _countEnemy;
        [SerializeField] private float _delayBetweenSpawn;
        [SerializeField] private float _delayOnEndWave;
        [SerializeField] private EnemyConfig _enemyConfig;

        public int CountEnemy => _countEnemy;
        public float DelayBetweenSpawn => _delayBetweenSpawn;
        public float DelayOnEndWave => _delayOnEndWave;
        public EnemyConfig EnemyConfig => _enemyConfig;
    }
}
