using UniRx;

namespace CodeBase.Infrastructure.Services
{
    public interface IGameStatusService
    {
        public void TrackWin();
        public void ResetStatus();
        public void SetStatus(GameStatus gameStatus);

        public IReactiveProperty<GameStatus> GameStatus { get; }
    }
}