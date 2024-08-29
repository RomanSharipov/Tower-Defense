using System.Collections.Generic;
using Assets.Scripts.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

namespace Assets.Scripts.CoreGamePlay
{
    public class EnemySpawner : MonoBehaviour
    {
        [Inject] private IEnemyFactory _enemyFactory;
        [Inject] private IBuildingService _buildingService;
        [Inject] private ICacherOfPath _cacherOfPath;

        [SerializeField] private TileView _start;
        [SerializeField] private TileView _target;

        private TileData[] _path;
        private bool _isSpawningEnabled;
        private float _spawnTimer;
        private int _counter;

        [SerializeField] private List<EnemyMovement> _enemiesOnBoard = new List<EnemyMovement>();
        [SerializeField] private float _interval = 2.0f; 

        public TileView StartTile => _start;
        public TileView TargetTile => _target;


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
                _spawnTimer += Time.deltaTime;

                if (_spawnTimer >= _interval)
                {
                    _spawnTimer = 0f;  
                    CreateEnemy().Forget();  
                }
            }
        }
        
        private async UniTask CreateEnemy()
        {
            Tank newEnemy = await _enemyFactory.CreateEnemy<Tank>(EnemyType.Tank);
            _counter++;
            newEnemy.transform.parent = transform;
            newEnemy.transform.localPosition = Vector3.zero;
            newEnemy.transform.gameObject.name = $"{_counter}.{gameObject.name}";
            newEnemy.Init(_path, _cacherOfPath,this);
            _enemiesOnBoard.Add(newEnemy.Movement);
        }
        
        private void OnDestroy()
        {
            StopSpawn();
        }
        
        private void OnEnable()
        {
            _buildingService.TurretIsBuilded += OnTurretIsBuilded;
        }
        
        private void OnDisable()
        {
            _buildingService.TurretIsBuilded -= OnTurretIsBuilded;
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
