using System;
using Assets.Scripts.CoreGamePlay;
using CodeBase.Configs;

namespace CodeBase.Infrastructure.Services
{
    public class WavesService : IWavesService
    {
        private WavesOnLevelData _wavesOnLevelData;
        private WaveData _currentWave;
        private int _spawned;
        private int _currentWaveIndex;
        private bool _allWavesIsOver;

        public WaveData CurrentWave => _currentWave;

        public bool AllWavesIsOver => _allWavesIsOver;

        public event Action<WaveData> WaveIsOver;
        

        public void SetNewWavesData(WavesOnLevelData wavesOnLevelData)
        {
            _allWavesIsOver = false;
            _wavesOnLevelData = wavesOnLevelData;
            _currentWaveIndex = 0;
            _spawned = 0;
            _currentWave = _wavesOnLevelData.WaveDatas[_currentWaveIndex];
        }

        public bool TryGetEnemy(out EnemyConfig enemyConfig)
        {
            enemyConfig = null;

            if (_allWavesIsOver)
                return false;

            if (!HasMoreEnemiesInCurrentWave())
            {
                ProceedToNextWave();
                return false;
            }
            _spawned++;

            if (_spawned >= _currentWave.CountEnemy)
            {
                WaveIsOver?.Invoke(_currentWave);
                ProceedToNextWave();
                return false;
            }
            enemyConfig = _currentWave.EnemyConfig;
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
            }
            else
            {
                _currentWaveIndex++;
                _currentWave = _wavesOnLevelData.WaveDatas[_currentWaveIndex];
            }
        }
    }
}
