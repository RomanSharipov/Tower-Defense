using System;
using CodeBase.Configs;
using UniRx;

namespace CodeBase.Infrastructure.Services
{
    public interface IWavesService
    {
        public WaveData CurrentWave { get; }
        public SubWaveData CurrentSubWave { get; }
        public bool AllWavesIsOver();
        public IObservable<int> OnNextWave { get; }
        public int AllWavesCount { get; }
        public int CurrentWaveNumber { get; }

        public event Action<WaveData> WaveIsOver;
        public event Action<SubWaveData> SubWaveIsOver;

        public void ProceedToNextWave();
        public void ResetWaves();
        public void Initialize(WavesOnLevelData wavesOnLevelData);
        public bool TryGetEnemy(out EnemyConfig enemyConfig);
    }
}
