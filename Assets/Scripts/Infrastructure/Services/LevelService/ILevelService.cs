using Assets.Scripts.CoreGamePlay;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services
{
    public interface ILevelService
    {
        public void IncreaseCurrentLevel();
        public UniTask<ILevelMain> LoadCurrentLevel();
        public void UnLoadCurrentLevel();
    }
}