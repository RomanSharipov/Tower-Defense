using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarodev_Pathfinding._Scripts;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class TileInitializer : MonoBehaviour
    {
        [SerializeField] private MapGenerator _mapGenerator;
        [SerializeField] private Movement _enemy;
        
        [SerializeField] private Tile _start;
        [SerializeField] private Tile _target;
        [SerializeField] private Transform[] path;


        private void Awake()
        {
            foreach (Tile tile in _mapGenerator.GameBoardTiles)
            {
                tile.InitializeNode(_mapGenerator.GameBoardTiles);
            }

            foreach (Tile tile in _mapGenerator.GameBoardTiles)
            {
                tile.NodeBase.CacheNeighbors();
            }
        }

        [ContextMenu("StartMovement()")]
        public void StartMovement()
        {
            List<NodeBase>  nodes = Pathfinding.FindPath(_start.NodeBase, _target.NodeBase);
            
            path = new Transform[nodes.Count];

            for (int i = 0; i < nodes.Count; i++)
            {
                path[i] = nodes[i].Tile.transform;
            }

            Array.Reverse(path);
            _enemy.Construct(path);
            _enemy.StartMovement();
        }

        [ContextMenu("SetStartPosition()")]
        private void SetStartPosition()
        {
            _enemy.transform.position = _start.transform.position;
        }
    }
}
