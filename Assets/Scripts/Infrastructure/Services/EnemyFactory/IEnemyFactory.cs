using System.Threading;
using Assets.Scripts.CoreGamePlay;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services
{
    public interface IEnemyFactory
    {
        public UniTask<EnemyBase> CreateEnemy(EnemyType enemyType, CancellationToken cancellationToken = default);
    }
}