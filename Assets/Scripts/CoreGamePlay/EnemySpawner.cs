using System;
using System.Collections.Generic;
using Assets.Scripts.Infrastructure.Services;
using CodeBase.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.CoreGamePlay
{
    public class EnemySpawner : MonoBehaviour
    {
        [Inject] private IEnemyFactory _enemyFactory;
        [Inject] private IBuildingService _buildingService;
        [Inject] private ICacherOfPath _cacherOfPath;
        [Inject] private IWavesService _wavesService;

        [SerializeField] private TileView _start;
        [SerializeField] private TileView _target;
        [SerializeField] private WavesOnLevelData _wavesOnLevelData;

        private List<TileData> _path;
        private bool _isSpawningEnabled;
        [SerializeField] private float _spawnTimer;
        private int _counter;

        [SerializeField] private List<EnemyMovement> _enemiesOnBoard = new List<EnemyMovement>();
        

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
        
        public void UpdateSpawnerPath()
        {
            _cacherOfPath.TrySetPath();
            _path = _cacherOfPath.Paths[this];
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
                newEnemy.transform.gameObject.name = $"{_counter}.{newEnemy.name}";
                newEnemy.Init(_path,_wavesService.CurrentWave.EnemyConfig);
                _enemiesOnBoard.Add(newEnemy.Movement);
            }
        }
        
        private void OnDestroy()
        {
            StopSpawn();
        }
        
        private void OnEnable()
        {
            _buildingService.TurretIsBuilded += OnTurretIsBuilded;
            _wavesService.WaveIsOver += OnWaveIsOver;
        }
        
        private void OnDisable()
        {
            _buildingService.TurretIsBuilded -= OnTurretIsBuilded;
            _wavesService.WaveIsOver -= OnWaveIsOver;
        }

        private void OnWaveIsOver(WaveData data)
        {
            _spawnTimer += 3.0f;
        }

        private void OnTurretIsBuilded(TurretBase turret, TileData tileData)
        {
            enabled = false;
            UpdateEnemiesPath(turret, tileData);
            UpdateSpawnerPath();
            enabled = true;
        }
        
        private void UpdateEnemiesPath(TurretBase turret, TileData tileData)
        {
            foreach (EnemyMovement enemy in _enemiesOnBoard)
            {
                enemy.UpdatePath(tileData); 
            }
        }
    }
}
