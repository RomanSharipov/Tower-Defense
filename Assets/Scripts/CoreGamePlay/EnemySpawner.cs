using System;
using System.Collections.Generic;
using System.Threading;
using CodeBase.Configs;
using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using NTC.Pool;
using UnityEngine;
using VContainer;
using UniRx;

namespace Assets.Scripts.CoreGamePlay
{
    public class EnemySpawner : MonoBehaviour
    {
        [Inject] private IEnemyFactory _enemyFactory;
        [Inject] private IGameStatusService _gameStatusService;
        [Inject] private IWavesService _wavesService;
        [Inject] private IAllEnemyStorage _allEnemyStorage;

        [SerializeField] private TileView _start;
        [SerializeField] private TileView _target;
       
        [SerializeField] private LayerMask _groundEnemy;
        [SerializeField] private float _spawnTimer;
        
        private DetectorGroundEnemies _detectorEnemies;
        private bool _isSpawningEnabled = false;
        private int _counter;

        public TileView StartTile => _start;
        public TileView TargetTile => _target;
        
        public void Init()
        {
            _detectorEnemies = new DetectorGroundEnemies(transform.position, _groundEnemy);
            _detectorEnemies.SetRadius(2.0f);

            _start.NodeBase.SetIsFreeStatus(false);
            _target.NodeBase.SetIsFreeStatus(false);
            
            _wavesService.OnNextWave.Subscribe(waveIndex =>
            {
                _spawnTimer = _wavesService.CurrentWave.DelayBetweenSpawn;
                StartSpawnEnemies();
            }).AddTo(this);
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
            if (_wavesService.TryGetEnemy(out EnemyConfig enemyConfig))
            {
                CancellationToken cancellationToken = this.GetCancellationTokenOnDestroy();

                EnemyBase newEnemy = await _enemyFactory.CreateEnemy(enemyConfig.EnemyType, cancellationToken).AttachExternalCancellation(cancellationToken);
                _counter++;
                newEnemy.transform.parent = transform;
                newEnemy.transform.localPosition = Vector3.zero;
                newEnemy.gameObject.name = $"{_counter}.{newEnemy.gameObject.name}";

                IHealth enemyHealth = new Health(enemyConfig.Health);

                newEnemy.Init(this, enemyConfig, enemyHealth);
                newEnemy.HealthBar.Init(enemyHealth);
                newEnemy.GoalIsReached += RemoveEnemy;
                newEnemy.Died += RemoveEnemy;
                _allEnemyStorage.Add(newEnemy);
            }
        }

        private void RemoveEnemy(EnemyBase enemy)
        {
            enemy.GoalIsReached -= RemoveEnemy;
            enemy.Died -= RemoveEnemy;
            _allEnemyStorage.Remove(enemy);
            NightPool.Despawn(enemy.gameObject);
            _gameStatusService.TrackWin();
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
