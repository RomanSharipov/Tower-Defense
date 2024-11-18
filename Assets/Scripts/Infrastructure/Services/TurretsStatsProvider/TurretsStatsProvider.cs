using VContainer;

namespace CodeBase.Infrastructure.Services
{
    public class TurretsStatsProvider : ITurretsStatsProvider
    {
        private readonly TurretsLevelsConfig _turretsLevelsConfig;

        [Inject]
        public TurretsStatsProvider(TurretsLevelsConfig turretsLevelsConfig)
        {
            _turretsLevelsConfig = turretsLevelsConfig;
        }
    }
}

