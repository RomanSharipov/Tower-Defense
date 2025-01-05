using Assets.Scripts.CoreGamePlay;

namespace CodeBase.Infrastructure.Services
{
    public interface ITurretPriceProvider
    {
        public int GetBuyPrice(TurretId turretId);
        public int GetUpgradePrice(TurretId turretId,int levelIndex);
    }
}
