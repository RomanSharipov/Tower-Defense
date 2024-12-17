using System;
using Assets.Scripts.CoreGamePlay;

namespace CodeBase.Infrastructure.Services
{
    public interface ITurretRemover
    {
        public event Action<TurretBase> TurretRemoved;
        public void TurretRemovedInvoke(TurretBase turretBase);
    }
}