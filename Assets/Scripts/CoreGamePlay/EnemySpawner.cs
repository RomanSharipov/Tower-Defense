using System;
using System.Collections.Generic;
using Assets.Scripts.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using Tarodev_Pathfinding._Scripts;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.CoreGamePlay
{
    public class EnemySpawner : MonoBehaviour
    {
        [Inject] private IEnemyFactory _enemyFactory;
        [Inject] private IBuildingService _buildingService;

        [SerializeField] private TileView _start;
        [SerializeField] private TileView _target;

        private TileData[] _path;
        private bool _isSpawningEnabled;
        private int _count = 0;
        private float _spawnTimer;

        [SerializeField] private List<EnemyMovement> _enemiesOnBoard = new List<EnemyMovement>();
        [SerializeField] private float _interval = 2.0f; // Время между спавнами врагов в секундах

        // Метод для начала спавна врагов
        [ContextMenu("StartSpawnEnemies")]
        public void StartSpawnEnemies()
        {
            _isSpawningEnabled = true;
            _spawnTimer = 0f;  // Сброс таймера спавна
        }

        // Метод для остановки спавна врагов
        [ContextMenu("StopSpawn")]
        public void StopSpawn()
        {
            _isSpawningEnabled = false;
        }

        private TileData[] GetStartPath()
        {
            List<TileData> nodes = Pathfinding.FindPath(_start.NodeBase, _target.NodeBase);
            TileData[] nodesArray = nodes.ToArray();

            Array.Reverse(nodesArray);

            return nodesArray;
        }

        // Метод инициализации пути
        public void UpdateSpawnerPath()
        {
            _path = GetStartPath();
            
        }

        // Основной метод Update, который будет проверять время и спавнить врагов
        private void Update()
        {
            if (_isSpawningEnabled)
            {
                _spawnTimer += Time.deltaTime;

                if (_spawnTimer >= _interval)
                {
                    _spawnTimer = 0f;  // Сброс таймера
                    CreateEnemy().Forget();  // Создание врага
                }
            }
        }

        // Метод для создания нового врага
        private async UniTask CreateEnemy()
        {
            Tank newEnemy = await _enemyFactory.CreateEnemy<Tank>(EnemyType.Tank); // Синхронное создание врага (так как UniTask убран)

            newEnemy.transform.parent = transform;
            newEnemy.transform.localPosition = Vector3.zero;
            _count++;
            Debug.Log("CreateEnemy");

            newEnemy.Init(_path, newEnemy.name);
            _enemiesOnBoard.Add(newEnemy.Movement);
        }

        // Метод для очистки ресурсов при уничтожении объекта
        private void OnDestroy()
        {
            StopSpawn();
        }

        // Подписка на события при включении объекта
        private void OnEnable()
        {
            _buildingService.TurretIsBuilded += OnTurretIsBuilded;
        }

        // Отписка от событий при отключении объекта
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

        // Метод для обновления пути врагов при постройке башни
        private void UpdateEnemiesPath(TurretBase turret, TileData tileData)
        {
            foreach (EnemyMovement enemy in _enemiesOnBoard)
            {
                enemy.UpdatePathIfNeeded(tileData); // Обновление пути для каждого врага
            }
        }
    }
}
