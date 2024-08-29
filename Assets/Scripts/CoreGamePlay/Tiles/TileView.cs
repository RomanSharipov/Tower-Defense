using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class TileView : MonoBehaviour
    {
        [SerializeField] private TileId _tileId;
        [SerializeField] private int _q;
        [SerializeField] private int _r;

        private TileData _nodeBase;
        
        public TileId TileId => _tileId;
        public TileData NodeBase => _nodeBase;

        public int Q => _q;
        public int R => _r;
        
        public void Construct(int q,int r)
        {
            _q = q;
            _r = r;
        }

        public void InitializeNode(List<TileView> gameBoardTiles)
        {
            bool walkable = _tileId == TileId.Empty;
            HexCoords hexCoords = new HexCoords(_q,_r);

            _nodeBase = new TileData(walkable, hexCoords, gameBoardTiles,this);
        }

        public void SetWalkable(TileId tileId)
        {
            _tileId = tileId;
        }

        public void UpdateTileData()
        {
            bool walkable = _tileId == TileId.Empty;
            _nodeBase.SetWalkable(walkable);
        }
    }
}
