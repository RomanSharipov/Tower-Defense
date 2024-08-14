using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public interface ITurretFactory
    {
        public UniTask<TileView> CreateTile(TileId TileId);
        public UniTask<T> CreateTurret<T>(TurretId turretType) where T : TurretBase;
        public void SetParrentTurret(Transform turrentParrent);
    }
}