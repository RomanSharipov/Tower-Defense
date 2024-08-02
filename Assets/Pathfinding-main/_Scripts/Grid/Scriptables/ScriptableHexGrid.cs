using System.Collections.Generic;
using _Scripts.Tiles;
using UnityEngine;

namespace Tarodev_Pathfinding._Scripts.Grid.Scriptables {
    [CreateAssetMenu(fileName = "New Scriptable Hex Grid")]
    public class ScriptableHexGrid : ScriptableGrid {

        [SerializeField,Range(1,50)] private int _gridWidth = 16;
        [SerializeField,Range(1,50)] private int _gridDepth = 9;
        
        public override Dictionary<Vector2, NodeBase> GenerateGrid() {
            var tiles = new Dictionary<Vector2, NodeBase>();
            GameObject grid = new GameObject {
                name = "Grid"
            };
            
            for (int r = 0; r < _gridDepth ; r++) {
                int rOffset = r >> 1;
                for (int q = -rOffset; q < _gridWidth - rOffset; q++) {
                    NodeBase tile = Instantiate(nodeBasePrefab,grid.transform);
                    tile.Init(DecideIfObstacle(), new HexCoords(q,r));
                    tiles.Add(tile.Coords.Pos,tile);
                    tile.gameObject.name = $"{tile.Coords.Pos.x}.{tile.Coords.Pos.y} {tile.gameObject.name}";
                }
            }
            grid.transform.rotation = Quaternion.Euler(90, 0, 0);
            return tiles;
        }
    }
}