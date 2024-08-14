using Cysharp.Threading.Tasks;

namespace Assets.Scripts.CoreGamePlay
{
    public interface ITurretFactory
    {
        public UniTask<TileView> CreateTile(TileId TileId);
    }
}