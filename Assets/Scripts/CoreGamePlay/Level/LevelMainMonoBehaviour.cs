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

        [SerializeField] private TileView[] _tiles;
        [SerializeField] private Transform _turretsParrent;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private TileView _start;
        [SerializeField] private TileView _target;
        
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
            _enemySpawner.enabled = false;
            _enemySpawner.Init(GetStartPath());
            _enemySpawner.enabled = true;
        }

        public void InitializeSceneServices()
        {
            foreach (TileView tile in _tiles)
            {
                tile.InitializeNode(_tiles.ToList());
            }

            foreach (TileView tile in _tiles)
            {
                tile.NodeBase.CacheNeighbors();
            }
            _enemySpawner.Init(GetStartPath());
            _enemySpawner.StartSpawnEnemies();
            _turretFactory.SetParrentTurret(_turretsParrent);
        }

        private TileData[] GetStartPath()
        {
            List<TileData> nodes = Pathfinding.FindPath(_start.NodeBase, _target.NodeBase);
            TileData[] nodesArray = nodes.ToArray();
            
            Array.Reverse(nodesArray);

            return nodesArray;
        }
    }
}
