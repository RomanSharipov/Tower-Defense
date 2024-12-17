using System;
using Assets.Scripts.CoreGamePlay;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services
{
    public interface IBuildingService
    {
        public event Action<TurretBase> TurretIsBuilded;

        public UniTask StartBuilding(TurretId turretId);
    }
}
