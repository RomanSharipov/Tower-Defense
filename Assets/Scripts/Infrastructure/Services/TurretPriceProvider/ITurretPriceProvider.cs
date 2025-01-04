using Assets.Scripts.CoreGamePlay;

namespace CodeBase.Infrastructure.Services
{
    public interface ITurretPriceProvider
    {
        public int GetPrice(TurretId turretId);
    }
}
