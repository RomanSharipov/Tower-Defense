using System;
using Assets.Scripts.CoreGamePlay;
using UniRx;

namespace CodeBase.Infrastructure.Services
{
    public class TurretRemover : ITurretRemover
    {
        public event Action<TurretBase> TurretRemoved;

        public void TurretRemovedInvoke(TurretBase turretBase)
        {
            turretBase.TileView.SetFreeStatus();

            TurretRemoved.Invoke(turretBase);
        }
    }
}
