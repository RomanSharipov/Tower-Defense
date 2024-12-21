using UniRx;

namespace CodeBase.Infrastructure.Services
{
    public interface IGameStatusService
    {
        public void TrackWin();
        public void TrackLose();
        public void ResetStatus();
        public IReactiveProperty<GameStatus> GameStatus { get; }
    }
}