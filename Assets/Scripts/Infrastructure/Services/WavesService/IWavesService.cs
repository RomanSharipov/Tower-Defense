using System;
using Assets.Scripts.CoreGamePlay;
using CodeBase.Configs;

namespace CodeBase.Infrastructure.Services
{
    public interface IWavesService
    {
        public WaveData CurrentWave { get; }
        public bool AllWavesIsOver { get; }

        public event Action<WaveData> WaveIsOver;
        

        public void SetNewWavesData(WavesOnLevelData wavesOnLevelData);
        public bool TryGetEnemy(out EnemyConfig enemyConfig);
    }
}
