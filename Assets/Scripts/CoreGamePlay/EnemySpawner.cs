using System.Collections.Generic;
using Assets.Scripts.Infrastructure.Services;
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

        [SerializeField] private TileView _start;
        [SerializeField] private TileView _target;

        private TileData[] _path;
        private bool _isSpawningEnabled;
        private int _count = 0;
        private float _spawnTimer;

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

            newEnemy.transform.parent = transform;
            newEnemy.transform.localPosition = Vector3.zero;
            _count++;
            
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
                enemy.UpdatePathIfNeeded(tileData); 
            }
        }
    }
}
