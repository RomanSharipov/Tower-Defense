using Assets.Scripts.CoreGamePlay;
using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
    public class TurretPriceProvider : ITurretPriceProvider
    {
        public int GetPrice(TurretId turretId)
        {
            return turretId switch
            {
                TurretId.Minigun => 15,
                TurretId.FlameTurret => 25,
                TurretId.Cannon => 35,
                TurretId.Slow => 45,
                TurretId.Rocket => 55,
                TurretId.AntiAir => 65,
                _ => LogUnknownTurretId(turretId)
            };
        }

        private int LogUnknownTurretId(TurretId turretId)
        {
            Debug.LogError($"TurretPriceProvider does not contain a price for turretId = {turretId}");
            return 0;
        }
    }
}
