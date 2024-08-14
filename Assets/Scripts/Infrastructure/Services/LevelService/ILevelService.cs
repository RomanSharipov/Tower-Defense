using Assets.Scripts.CoreGamePlay;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Infrastructure.Services
{
    public interface ILevelService
    {
        public UniTask<ILevelMain> LoadCurrentLevel();
        public void UnLoadCurrentLevel();
    }
}