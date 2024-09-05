using System;
using Assets.Scripts.CoreGamePlay;

namespace CodeBase.Configs
{
    [Serializable]
    public class WaveData
    {
        public EnemyType EnemyType;
        public int Count;
        public float DelayBetweenSpawn;
    }
}
