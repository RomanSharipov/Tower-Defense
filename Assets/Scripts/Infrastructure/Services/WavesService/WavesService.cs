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
        private SubWaveData _currentSubWave;
        private int _spawnedInSubWave;
        private int _currentWaveIndex;
        private int _currentSubWaveIndex;
        private int _currentWaveNumber;
        private int _currentSubWaveNumber;

        private Subject<int> _onNextWave = new();

        public WaveData CurrentWave => _currentWave;
        public SubWaveData CurrentSubWave => _currentSubWave;
        public int AllWavesCount => _wavesOnLevelData.WaveDatas.Length;
        public int CurrentWaveNumber => _currentWaveNumber;

        public event Action<WaveData> WaveIsOver;
        public event Action<SubWaveData> SubWaveIsOver;

        public IObservable<int> OnNextWave => _onNextWave;

        public void Initialize(WavesOnLevelData wavesOnLevelData)
        {
            _wavesOnLevelData = wavesOnLevelData;
            
            _currentWave = _wavesOnLevelData.WaveDatas[_currentWaveIndex];
            _currentSubWave = _currentWave.SubWaveData[_currentSubWaveIndex];
        }

        public bool TryGetEnemy(out EnemyConfig enemyConfig)
        {
            enemyConfig = null;

            if (_spawnedInSubWave >= _currentSubWave.CountEnemy)
            {
                SubWaveIsOver?.Invoke(_currentSubWave);

                if (!ProceedToNextSubWave())
                {
                    WaveIsOver?.Invoke(_currentWave);
                    return false;
                }
            }

            if (EnemiesInSubWaveAreOver())
                return false;

            _spawnedInSubWave++;
            enemyConfig = _currentSubWave.EnemyConfig;
            return true;
        }

        private bool EnemiesInSubWaveAreOver()
        {
            return _spawnedInSubWave >= _currentSubWave.CountEnemy;
        }

        public void ProceedToNextWave()
        {
            if (AllWavesIsOver())
                return;

            _spawnedInSubWave = 0;
            _currentWaveNumber++;
            _currentWaveIndex = _currentWaveNumber - 1;
            _currentWave = _wavesOnLevelData.WaveDatas[_currentWaveIndex];
            _currentSubWaveIndex = 0;

            ResetSubWaves();
            ProceedToNextSubWave();
            _onNextWave.OnNext(_currentWaveNumber);
        }

        private bool ProceedToNextSubWave()
        {
            if (_currentSubWaveIndex + 1 >= _currentWave.SubWaveData.Length)
                return false;

            _spawnedInSubWave = 0;
            _currentSubWaveNumber++;
            _currentSubWaveIndex = _currentSubWaveNumber - 1;

            _currentSubWave = _currentWave.SubWaveData[_currentSubWaveIndex];
            
            return true;
        }

        public bool AllWavesIsOver()
        {
            if (_currentWaveIndex + 1 >= _wavesOnLevelData.WaveDatas.Length)
            {
                if (_currentSubWaveIndex + 1 >= _currentWave.SubWaveData.Length)
                    return true;
            }

            return false;
        }

        public void ResetWaves()
        {
            _currentWave = null;
            _currentWaveNumber = 0;
            _currentWaveIndex = 0;

            ResetSubWaves();
        }
        public void ResetSubWaves()
        {
            _spawnedInSubWave = 0;
            _currentSubWave = null;
            _currentSubWaveIndex = 0;
            _currentSubWaveNumber = 0;
        }
    }
}
