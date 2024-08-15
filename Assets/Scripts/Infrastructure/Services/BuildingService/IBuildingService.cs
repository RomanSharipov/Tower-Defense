using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Infrastructure.Services
{
    public interface IBuildingService
    {
        UniTask StartBuilding();
    }
}
