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
        
        private void Awake()
        {
            foreach (Tile tile in _mapGenerator.GameBoardTiles)
            {
                tile.InitializeNode(_mapGenerator.GameBoardTiles);
            }
        }

        [ContextMenu("StartMovement()")]
        public void StartMovement()
        {
            List<NodeBase> nodes = Pathfinding.FindPath(_start.NodeBase,_target.NodeBase);

            Transform[] path = new Transform[nodes.Count];

            for (int i = 0; i < nodes.Count; i++)
            {
                path[i] = nodes[i].Tile.transform;
            }
            _enemy.Construct(path);
            _enemy.StartMovement();
        }
    }
}
