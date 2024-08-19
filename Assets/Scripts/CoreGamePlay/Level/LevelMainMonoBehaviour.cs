using System.Collections.Generic;
using System;
using System.Linq;
using Tarodev_Pathfinding._Scripts;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.CoreGamePlay
{
    public class LevelMainMonoBehaviour : MonoBehaviour, ILevelMain
    {
        [Inject] private ITurretFactory _turretFactory;

        [SerializeField] private TileView[] _tiles;
        [SerializeField] private Transform _turretsParrent;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private TileView _start;
        [SerializeField] private TileView _target;

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
            _enemySpawner.StartSpawnEnemies().Forget();
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
