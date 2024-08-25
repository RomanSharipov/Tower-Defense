using System.Collections.Generic;

namespace Assets.Scripts.CoreGamePlay
{
    public class TilesStorage : ITilesStorage
    {
        public IReadOnlyCollection<TileData> Tiles => _tileDatas;

        private TileData[] _tileDatas;

        public void SetTiles(TileData[] tileDatas)
        {
            _tileDatas = tileDatas;
        }
    }
}
