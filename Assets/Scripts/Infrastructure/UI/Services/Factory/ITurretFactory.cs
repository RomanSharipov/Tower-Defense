using Assets.Scripts.CoreGamePlay;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.UI.Services
{
    public interface ITurretFactory
    {
        public UniTask<TileView> CreateTile(TileId TileId);
    }
}