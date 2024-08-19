using System;
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
        private TileData[] _path;
        private bool _enabled;
        private CancellationTokenSource _spawningJob;

        [ContextMenu("StartSpawnEnemies")]
        public async UniTaskVoid StartSpawnEnemies()
        {
            _enabled = true;

            while (_enabled)
            {
                CreateEnemyAsync().Forget();
                _spawningJob = new CancellationTokenSource();
                await UniTask.Delay(TimeSpan.FromSeconds(1.0f),cancellationToken: _spawningJob.Token);
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
            newEnemy.transform.SetParent(transform);
            newEnemy.transform.localPosition = Vector3.zero;
            newEnemy.Init(_path);
        }

        private void OnDestroy()
        {
            StopSpawn();
        }
    }
}
