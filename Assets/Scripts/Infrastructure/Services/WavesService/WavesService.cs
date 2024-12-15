using System;
using Assets.Scripts.CoreGamePlay;
using CodeBase.Configs;
using UniRx;
using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
    public class WavesService : IWavesService
    {
        private WavesOnLevelData _wavesOnLevelData;
        private WaveData _currentWave;
        private int _spawned;
        private Subject<int> _onNextWave = new();
        private int _currentWaveIndex;
        private int _currentWaveNumber;
        private bool _allWavesIsOver;

        public WaveData CurrentWave => _currentWave;
        public int AllWavesCount => _wavesOnLevelData.WaveDatas.Length;

        public bool AllWavesIsOver => _allWavesIsOver;
        public int CurrentWaveNumber => _currentWaveNumber;

        public event Action<WaveData> WaveIsOver;

        public IObservable<int> OnNextWave  => _onNextWave;
        

        public void Initialize(WavesOnLevelData wavesOnLevelData)
        {
            _allWavesIsOver = false;
            _wavesOnLevelData = wavesOnLevelData;
            _spawned = 0;
        }

        public bool TryGetEnemy(out EnemyConfig enemyConfig)
        {
            enemyConfig = null;

            if (_allWavesIsOver)
                return false;

            if (!HasMoreEnemiesInCurrentWave())
            {
                return false;
            }
            _spawned++;

            if (_spawned >= _currentWave.CountEnemy)
            {
                WaveIsOver?.Invoke(_currentWave);
                return false;
            }
            enemyConfig = _currentWave.EnemyConfig;
            return true;
        }

        private bool HasMoreEnemiesInCurrentWave()
        {
            return _spawned < _currentWave.CountEnemy;
        }

        public void ProceedToNextWave()
        {
            _spawned = 0;

            if (_currentWaveIndex + 1 >= _wavesOnLevelData.WaveDatas.Length)
            {
                _allWavesIsOver = true;
                return;
            }
            else
            {
                _currentWaveNumber++;
                _currentWaveIndex = _currentWaveNumber - 1;
                _currentWave = _wavesOnLevelData.WaveDatas[_currentWaveIndex];
            }
            _onNextWave.OnNext(_currentWaveNumber);
        }
    }
}
