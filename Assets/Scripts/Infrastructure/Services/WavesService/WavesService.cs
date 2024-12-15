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
        
        public WaveData CurrentWave => _currentWave;
        public int AllWavesCount => _wavesOnLevelData.WaveDatas.Length;
        public int CurrentWaveNumber => _currentWaveNumber;

        public event Action<WaveData> WaveIsOver;

        public IObservable<int> OnNextWave  => _onNextWave;
        

        public void Initialize(WavesOnLevelData wavesOnLevelData)
        {
            _wavesOnLevelData = wavesOnLevelData;
            _spawned = 0;
        }

        public bool TryGetEnemy(out EnemyConfig enemyConfig)
        {
            enemyConfig = null;

            if (_spawned >= _currentWave.CountEnemy)
            {
                WaveIsOver?.Invoke(_currentWave);
                return false;
            }

            if (EnemiesIsOver())
                return false;

            _spawned++;
            
            enemyConfig = _currentWave.EnemyConfig;
            return true;
        }

        private bool EnemiesIsOver()
        {
            return _spawned >= _currentWave.CountEnemy;
        }

        public void ProceedToNextWave()
        {
            if (AllWavesIsOver())
                return;

            _spawned = 0;
            _currentWaveNumber++;
            _currentWaveIndex = _currentWaveNumber - 1;
            _currentWave = _wavesOnLevelData.WaveDatas[_currentWaveIndex];
            _onNextWave.OnNext(_currentWaveNumber);
        }

        public bool AllWavesIsOver()
        {
            return _currentWaveIndex + 1 >= _wavesOnLevelData.WaveDatas.Length;
        }

        public void ResetWaves()
        {
            _currentWave = null;
            _currentWaveIndex = 0;
            _currentWaveNumber = 0;
        }
    }
}
