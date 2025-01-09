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
            _spawnedInSubWave = 0;
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
            _currentSubWaveIndex = -1;

            ProceedToNextSubWave();
            _onNextWave.OnNext(_currentWaveNumber);
        }

        private bool ProceedToNextSubWave()
        {
            _currentSubWaveIndex++;

            if (_currentSubWaveIndex >= _currentWave.SubWaveData.Length)
                return false;

            _currentSubWave = _currentWave.SubWaveData[_currentSubWaveIndex];
            _spawnedInSubWave = 0;
            return true;
        }

        public bool AllWavesIsOver()
        {
            return _currentWaveIndex + 1 >= _wavesOnLevelData.WaveDatas.Length;
        }

        public void ResetWaves()
        {
            _currentWave = null;
            _currentSubWave = null;
            _currentWaveIndex = 0;
            _currentSubWaveIndex = -1;
            _currentWaveNumber = 0;
            _spawnedInSubWave = 0;
        }
    }
}
