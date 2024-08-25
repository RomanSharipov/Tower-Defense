using System.Collections.Generic;
using System;
using System.Linq;
using Tarodev_Pathfinding._Scripts;
using UnityEngine;
using VContainer;
using Assets.Scripts.Infrastructure.Services;

namespace Assets.Scripts.CoreGamePlay
{
    public class LevelMainMonoBehaviour : MonoBehaviour, ILevelMain
    {
        [Inject] private ITurretFactory _turretFactory;
        [Inject] private IBuildingService _buildingService;
        [Inject] private ICacherOfPath _cacherOfPath;
        [Inject] private ITilesStorage _tilesStorage;

        [SerializeField] private TileView[] _tiles;
        [SerializeField] private Transform _turretsParrent;
        [SerializeField] private EnemySpawner[] _enemySpawners;

        
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

        }

        public void InitializeSceneServices()
        {
            TileData[] tilesData = new TileData[_tiles.Length];

            for (int i = 0; i < _tiles.Length; i++)
            {
                tilesData[i] = _tiles[i].NodeBase;
            }
            _tilesStorage.SetTiles(tilesData);

            _cacherOfPath.SetSpawnersOnCurrentLevel(_enemySpawners);
            foreach (TileView tile in _tiles)
            {
                tile.InitializeNode(_tiles.ToList());
            }

            foreach (TileView tile in _tiles)
            {
                tile.NodeBase.CacheNeighbors();
            }

            _turretFactory.SetParrentTurret(_turretsParrent);
            foreach (EnemySpawner enemySpawner in _enemySpawners)
            {
                enemySpawner.UpdateSpawnerPath();
                enemySpawner.StartSpawnEnemies();
            }
            _cacherOfPath.TrySetPath();
        }
    }
}
