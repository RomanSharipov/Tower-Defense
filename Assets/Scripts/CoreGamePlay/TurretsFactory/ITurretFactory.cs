using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public interface ITurretFactory
    {
        public UniTask<TileView> CreateTile(TileId TileId);
        public UniTask<TurretBase> CreateTurret(TurretId turretType);
        public void SetParrentTurret(Transform turrentParrent);
    }
}