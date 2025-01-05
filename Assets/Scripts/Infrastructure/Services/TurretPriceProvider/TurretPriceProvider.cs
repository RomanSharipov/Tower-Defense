using Assets.Scripts.CoreGamePlay;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure.Services
{
    public class TurretPriceProvider : ITurretPriceProvider
    {
        private readonly TurretsPriceConfig _turretsPriceConfig;

        [Inject]
        public TurretPriceProvider(TurretsPriceConfig turretsPriceConfig)
        {
            _turretsPriceConfig = turretsPriceConfig;
        }
        
        public int GetBuyPrice(TurretId turretId)
        {
            return turretId switch
            {
                TurretId.Minigun => _turretsPriceConfig.Minigun,
                TurretId.FlameTurret => _turretsPriceConfig.FlameTurret,
                TurretId.Cannon => _turretsPriceConfig.Cannon,
                TurretId.Slow => _turretsPriceConfig.Slow,
                TurretId.Rocket => _turretsPriceConfig.Rocket,
                TurretId.AntiAir => _turretsPriceConfig.AntiAir,
                _ => LogUnknownTurretId(turretId)
            };
        }

        public int GetUpgradePrice(TurretId turretId, int levelIndex)
        {
            try
            {
                return turretId switch
                {
                    TurretId.Minigun => _turretsPriceConfig.MinigunUpgradePrices[levelIndex],
                    TurretId.FlameTurret => _turretsPriceConfig.FlameTurretUpgradePrices[levelIndex],
                    TurretId.Cannon => _turretsPriceConfig.CannonUpgradePrices[levelIndex],
                    TurretId.Slow => _turretsPriceConfig.SlowUpgradePrices[levelIndex],
                    TurretId.Rocket => _turretsPriceConfig.RocketUpgradePrices[levelIndex],
                    TurretId.AntiAir => _turretsPriceConfig.AntiAirUpgradePrices[levelIndex],
                    _ => LogUnknownTurretId(turretId)
                };
            }
            catch (System.Exception)
            {

                throw;
            }


        }

        private int LogUnknownTurretId(TurretId turretId)
        {
            Debug.LogError($"TurretPriceProvider does not contain turretId = {turretId}");
            return 0;
        }
    }
}
