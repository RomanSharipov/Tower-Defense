using Assets.Scripts.CoreGamePlay;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services
{
    public interface ILevelService
    {
        public UniTask<ILevelMain> LoadCurrentLevel();
        public void UnLoadCurrentLevel();
    }
}