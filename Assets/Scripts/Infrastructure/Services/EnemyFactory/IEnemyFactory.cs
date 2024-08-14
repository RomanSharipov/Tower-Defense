using Assets.Scripts.CoreGamePlay;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Infrastructure.Services
{
    public interface IEnemyFactory
    {
        public UniTask<T> CreateEnemy<T>(EnemyType windowType) where T : EnemyBase;
    }
}