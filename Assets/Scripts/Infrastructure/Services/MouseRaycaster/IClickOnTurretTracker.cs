using System;
using Assets.Scripts.CoreGamePlay;

namespace CodeBase.Infrastructure.Services
{
    public interface IClickOnTurretTracker
    {
        public IObservable<TurretBase> ClickOnTurret { get; }

        public void EndTracking();
        public void StartTracking();
    }
}