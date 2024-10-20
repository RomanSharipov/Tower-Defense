using System.Collections.Generic;
using BezierSolution;
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
        
       
        [Inject] private IWavesService _wavesService;

        [SerializeField] private TileView _start;
        [SerializeField] private TileView _target;
        [SerializeField] private WavesOnLevelData _wavesOnLevelData;
        

        
        private bool _isSpawningEnabled;
        [SerializeField] private float _spawnTimer;
        private int _counter;

        [SerializeField] private List<EnemyBase> _enemiesOnBoard = new List<EnemyBase>();


        public TileView StartTile => _start;
        public TileView TargetTile => _target;

        private void Awake()
        {
            _wavesService.SetNewWavesData(_wavesOnLevelData);
            _spawnTimer = _wavesService.CurrentWave.DelayBetweenSpawn;
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
            if (_isSpawningEnabled)
            {
                _spawnTimer -= Time.deltaTime;

                if (_spawnTimer <= 0f)
                {
                    _spawnTimer = _wavesService.CurrentWave.DelayBetweenSpawn;
                    CreateEnemyIfNeeded().Forget();
                }
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

                IEnemyHealth enemyHealth = new EnemyHealth(_wavesService.CurrentWave.EnemyConfig.Health);

                newEnemy.Init(this, _wavesService.CurrentWave.EnemyConfig, enemyHealth);
                newEnemy.HealthBar.Init(enemyHealth);
                newEnemy.GoalIsReached += OnGoalIsReached;
                _enemiesOnBoard.Add(newEnemy);
            }
        }

        private void OnGoalIsReached(EnemyBase enemy)
        {
            enemy.GoalIsReached -= OnGoalIsReached;
            _enemiesOnBoard.Remove(enemy);
            NightPool.Despawn(enemy.gameObject);
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
            _spawnTimer += 3.0f;
        }

        private void OnTurretIsBuilded(TurretBase turret, TileData tileData)
        {
            enabled = false;
            //UpdateSpawnerPath();
            enabled = true;
        }
    }
}
