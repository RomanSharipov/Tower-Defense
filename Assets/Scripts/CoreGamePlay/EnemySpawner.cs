using System.Collections.Generic;
using CodeBase.Configs;
using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using NTC.Pool;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.CoreGamePlay
{
    public class EnemySpawner : MonoBehaviour
    {
        [Inject] private IEnemyFactory _enemyFactory;
        [Inject] private IPlayerWinTracker _playerWinTracker;
        [Inject] private IWavesService _wavesService;

        [SerializeField] private TileView _start;
        [SerializeField] private TileView _target;
        [SerializeField] private WavesOnLevelData _wavesOnLevelData;
        [SerializeField] private LayerMask _groundEnemy;
        [SerializeField] private float _spawnTimer;
        [SerializeField] private List<EnemyBase> _enemiesOnBoard = new List<EnemyBase>();
        
        private DetectorGroundEnemies _detectorEnemies;
        private bool _isSpawningEnabled = false;
        private int _counter;
        
        public TileView StartTile => _start;
        public TileView TargetTile => _target;

        public void Init()
        {
            _wavesService.SetNewWavesData(_wavesOnLevelData);
            _spawnTimer = _wavesService.CurrentWave.DelayBetweenSpawn;
            _detectorEnemies = new DetectorGroundEnemies(transform.position, _groundEnemy);
            _detectorEnemies.SetRadius(2.0f);

            _start.NodeBase.SetIsFreeStatus(false);
            _target.NodeBase.SetIsFreeStatus(false);
            _isSpawningEnabled = true;
        }
        
        public void StartSpawnEnemies()
        {
            _isSpawningEnabled = true;
            _spawnTimer = 0f;
        }

        public void StopSpawn()
        {
            _isSpawningEnabled = false;
        }


        
        private void Update()
        {
            if (!_isSpawningEnabled)
                return;

            _spawnTimer -= Time.deltaTime;

            if (_spawnTimer <= 0f)
            {
                _spawnTimer = _wavesService.CurrentWave.DelayBetweenSpawn;
                if (_detectorEnemies.TryFindEnemy(out EnemyBase enemy))
                {
                    _spawnTimer += Time.deltaTime;
                    return;
                }
                CreateEnemyIfNeeded().Forget();
            }

        }

        private async UniTask CreateEnemyIfNeeded()
        {
            if (_wavesService.TryGetEnemy(out EnemyType enemyType))
            {
                EnemyBase newEnemy = await _enemyFactory.CreateEnemy(enemyType);
                _counter++;
                newEnemy.transform.parent = transform;
                newEnemy.transform.localPosition = Vector3.zero;
                newEnemy.gameObject.name = $"{_counter}.{newEnemy.gameObject.name}";

                IEnemyHealth enemyHealth = new EnemyHealth(_wavesService.CurrentWave.EnemyConfig.Health);

                newEnemy.Init(this, _wavesService.CurrentWave.EnemyConfig, enemyHealth);
                newEnemy.HealthBar.Init(enemyHealth);
                newEnemy.GoalIsReached += RemoveEnemy;
                newEnemy.Died += RemoveEnemy;
                _enemiesOnBoard.Add(newEnemy);
            }
        }
        
        private void RemoveEnemy(EnemyBase enemy)
        {
            enemy.GoalIsReached -= RemoveEnemy;
            enemy.Died -= RemoveEnemy;
            _enemiesOnBoard.Remove(enemy);
            NightPool.Despawn(enemy.gameObject);
            _playerWinTracker.CheckWin(_enemiesOnBoard.Count);
        }

        private void OnDestroy()
        {
            StopSpawn();
        }

        private void OnEnable()
        {
            _wavesService.WaveIsOver += OnWaveIsOver;
        }

        private void OnDisable()
        {
            _wavesService.WaveIsOver -= OnWaveIsOver;
        }

        private void OnWaveIsOver(WaveData data)
        {
            _spawnTimer += data.DelayOnEndWave;
        }
    }
}
