using Assets.Scripts.CoreGamePlay.Enemy;
using CodeBase.Infrastructure.UI;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Infrastructure.Services
{
    public interface IEnemyFactory
    {
        public UniTask<T> CreateEnemy<T>(EnemyType windowType) where T : EnemyBase;
    }
}