using UniRx;

namespace Assets.Scripts.CoreGamePlay
{
    public interface IEnemyHealth
    {
        public IReactiveProperty<int> CurrentHealth { get; }
        public int MaxHealth { get; }
    }
}