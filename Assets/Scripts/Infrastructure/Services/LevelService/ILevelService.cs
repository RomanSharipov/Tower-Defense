using Assets.Scripts.CoreGamePlay;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services
{
    public interface ILevelService
    {
        public void IncreaseCurrentLevel();
        public UniTask<ISceneInitializer> LoadCurrentLevel();
        public void UnLoadCurrentLevel();
    }
}