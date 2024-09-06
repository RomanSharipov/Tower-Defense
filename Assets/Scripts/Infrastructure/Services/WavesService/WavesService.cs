using System;
using Assets.Scripts.CoreGamePlay;
using CodeBase.Configs;

namespace Assets.Scripts.Infrastructure.Services
{
    public class WavesService : IWavesService
    {
        private WavesOnLevelData _wavesOnLevelData;
        private WaveData _currentWave;
        private int _spawned;
        private int _currentWaveIndex;
        private bool _allWavesIsOver;

        public WaveData CurrentWave => _currentWave;
        public event Action<WaveData> WaveIsOver;
        public event Action AllWavesIsOver;

        public void SetNewWavesData(WavesOnLevelData wavesOnLevelData)
        {
            _allWavesIsOver = false;
            _wavesOnLevelData = wavesOnLevelData;
            _currentWaveIndex = 0;
            _spawned = 0;
            _currentWave = _wavesOnLevelData.WaveDatas[_currentWaveIndex];
        }

        public bool TryGetEnemy(out EnemyType enemyType)
        {
            if (_allWavesIsOver)
            {
                enemyType = EnemyType.None;
                return false;
            }

            if (!HasMoreEnemiesInCurrentWave())
            {
                ProceedToNextWave();
                enemyType = EnemyType.None;
                return false;
            }

            enemyType = _currentWave.EnemyType;
            _spawned++;

            if (_spawned >= _currentWave.CountEnemy)
            {
                WaveIsOver?.Invoke(_currentWave);
                ProceedToNextWave();
            }

            return true;
        }

        private bool HasMoreEnemiesInCurrentWave()
        {
            return _spawned < _currentWave.CountEnemy;
        }

        private void ProceedToNextWave()
        {
            _spawned = 0;

            if (_currentWaveIndex + 1 >= _wavesOnLevelData.WaveDatas.Length)
            {
                _allWavesIsOver = true;
                AllWavesIsOver?.Invoke();
            }
            else
            {
                _currentWaveIndex++;
                _currentWave = _wavesOnLevelData.WaveDatas[_currentWaveIndex];
            }
        }
    }

    public interface IWavesService
    {
        public WaveData CurrentWave { get; }
        public void SetNewWavesData(WavesOnLevelData wavesOnLevelData);
        public bool TryGetEnemy(out EnemyType enemyType);
    }
}
