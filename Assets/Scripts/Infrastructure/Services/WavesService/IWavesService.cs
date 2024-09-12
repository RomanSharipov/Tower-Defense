using System;
using Assets.Scripts.CoreGamePlay;
using CodeBase.Configs;

namespace CodeBase.Infrastructure.Services
{
    public interface IWavesService
    {
        public WaveData CurrentWave { get; }

        public event Action<WaveData> WaveIsOver;
        public event Action AllWavesIsOver;

        public void SetNewWavesData(WavesOnLevelData wavesOnLevelData);
        public bool TryGetEnemy(out EnemyType enemyType);
    }
}
