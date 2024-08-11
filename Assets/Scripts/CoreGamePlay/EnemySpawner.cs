using System.Threading.Tasks;
using Assets.Scripts.CoreGamePlay.Enemy;
using Assets.Scripts.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.CoreGamePlay
{
    public class EnemySpawner : MonoBehaviour
    {
        [Inject] private IEnemyFactory _enemyFactory;

        [ContextMenu("CreateEnemy")]
        public void CreateEnemy()
        {
            CreateEnemyAsync().Forget();
        }

        private async UniTaskVoid CreateEnemyAsync()
        {
            Tank newEnemy = await _enemyFactory.CreateEnemy<Tank>(EnemyType.Tank);
            newEnemy.transform.SetParent(transform);
        }
    }
}
