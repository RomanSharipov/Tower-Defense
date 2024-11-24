using VContainer;

namespace CodeBase.Infrastructure.Services
{
    public class TurretsStatsProvider : ITurretsStatsProvider
    {
        private readonly TurretsLevelsConfig _turretsLevelsConfig;

        public CannonLevelData CannonLevelData => _turretsLevelsConfig.CannonLevelData;

        [Inject]
        public TurretsStatsProvider(TurretsLevelsConfig turretsLevelsConfig)
        {
            _turretsLevelsConfig = turretsLevelsConfig;
        }
    }
}

