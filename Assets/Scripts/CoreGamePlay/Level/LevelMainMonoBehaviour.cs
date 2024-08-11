using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay.Level
{
    public class LevelMainMonoBehaviour : MonoBehaviour
    {
        [SerializeField] private Tile[] _tiles;
        [SerializeField] private EnemySpawner _enemySpawner;

        private void Awake()
        {
            InitializeTiles();
        }

        private void InitializeTiles()
        {
            foreach (Tile tile in _tiles)
            {
                tile.InitializeNode(_tiles.ToList());
            }

            foreach (Tile tile in _tiles)
            {
                tile.NodeBase.CacheNeighbors();
            }
        }
    }
}
