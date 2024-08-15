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

        private Vector3[] GetStartPath()
        {
            float yOffset = 0.41f;

            List<TileData> nodes = Pathfinding.FindPath(_start.NodeBase, _target.NodeBase);

            Vector3[] path = new Vector3[nodes.Count];

            for (int i = 0; i < nodes.Count; i++)
            {
                path[i] = nodes[i].Tile.transform.position + Vector3.up * yOffset;
            }

            Array.Reverse(path);

            return path;
        }
    }
}
