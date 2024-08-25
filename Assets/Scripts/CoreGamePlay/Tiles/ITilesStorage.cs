using System.Collections.Generic;

namespace Assets.Scripts.CoreGamePlay
{
    public interface ITilesStorage
    {
        public IReadOnlyCollection<TileData> Tiles { get; }

        public void SetTiles(TileData[] tileDatas);
    }
}