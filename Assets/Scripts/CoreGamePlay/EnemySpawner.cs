using System;
using System.Collections.Generic;
using System.Threading;
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
        private TileData[] _path;
        private bool _enabled;
        private int _count=0;
        private CancellationTokenSource _spawningJob;
        [SerializeField] private List<EnemyBase> _enemiesOnBoard = new List<EnemyBase>();

        [ContextMenu("StartSpawnEnemies")]
        public async UniTaskVoid StartSpawnEnemies()
        {
            _enabled = true;
            _spawningJob?.Cancel();
            while (_enabled)
            {
                CreateEnemyAsync().Forget();
                _spawningJob = new CancellationTokenSource();
                await UniTask.Delay(TimeSpan.FromSeconds(3.0f),cancellationToken: _spawningJob.Token);
            }
        }        
        [ContextMenu("StopSpawn")]
        public void StopSpawn()
        {
            _enabled = false;
            _spawningJob?.Cancel();
        }

        public void Init(TileData[] path)
        {
            _path = path;
        }

        private async UniTaskVoid CreateEnemyAsync()
        {
            Tank newEnemy = await _enemyFactory.CreateEnemy<Tank>(EnemyType.Tank);
            
            newEnemy.transform.parent = transform;
            newEnemy.transform.localPosition = Vector3.zero;
            _count++;
            
            newEnemy.Init(_path, newEnemy.name);
            _enemiesOnBoard.Add(newEnemy);
        }

        private void OnDestroy()
        {
            StopSpawn();
            _spawningJob?.Dispose();
            _spawningJob = null;
        }
        
        private void OnEnable()
        {
            _buildingService.TurretIsBuilded += UpdateEnemiesPath;
        }
        private void OnDisable()
        {
            _buildingService.TurretIsBuilded -= UpdateEnemiesPath;
        }

        private void UpdateEnemiesPath(TurretBase turret, TileData tileData)
        {
            foreach (EnemyBase enemy in _enemiesOnBoard)
            {
                enemy.UpdatePathIfNeeded(tileData);
            }
        }
    }
}
