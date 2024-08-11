using System.Collections.Generic;
using System;
using System.Linq;
using Tarodev_Pathfinding._Scripts;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay.Level
{
    public class LevelMainMonoBehaviour : MonoBehaviour, ILevelMain
    {
        [SerializeField] private Tile[] _tiles;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private Tile _start;
        [SerializeField] private Tile _target;

        public void InitializeSceneServices()
        {
            foreach (Tile tile in _tiles)
            {
                tile.InitializeNode(_tiles.ToList());
            }

            foreach (Tile tile in _tiles)
            {
                tile.NodeBase.CacheNeighbors();
            }
            _enemySpawner.Init(GetStartPath());
            _enemySpawner.StartSpawnEnemies().Forget();
        }

        private Transform[] GetStartPath()
        {
            List<NodeBase> nodes = Pathfinding.FindPath(_start.NodeBase, _target.NodeBase);

            Transform [] path = new Transform[nodes.Count];

            for (int i = 0; i < nodes.Count; i++)
            {
                path[i] = nodes[i].Tile.transform;
            }

            Array.Reverse(path);

            return path;
        }
    }
}
