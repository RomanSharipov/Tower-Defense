using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TileId _tileId;
        [SerializeField] private int _q;
        [SerializeField] private int _r;

        private NodeBase _nodeBase;
        
        public TileId TileId => _tileId;
        public NodeBase NodeBase => _nodeBase;

        public int Q => _q;
        public int R => _r;
        
        public void Construct(int q,int r)
        {
            _q = q;
            _r = r;
        }

        public void InitializeNode(List<Tile> gameBoardTiles)
        {
            bool walkable = _tileId == TileId.Empty;
            HexCoords hexCoords = new HexCoords(_q,_r);

            _nodeBase = new NodeBase(walkable, hexCoords, gameBoardTiles,this);
        }

        public void UpdateWalkable(TileId tileId)
        {
            _tileId = tileId;
        }
    }
}
