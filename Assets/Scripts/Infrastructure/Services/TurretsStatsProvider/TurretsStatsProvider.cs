using VContainer;

namespace CodeBase.Infrastructure.Services
{
    public class TurretsStatsProvider : ITurretsStatsProvider
    {
        private readonly TurretsLevelsConfig _turretsLevelsConfig;

        public CannonLevelData CannonLevelData => _turretsLevelsConfig.CannonLevelData;
        public MinigunLevelData MinigunLevelData => _turretsLevelsConfig.MinigunLevelData;
        public AntiAirLevelData AntiAirLevelData => _turretsLevelsConfig.AntiAirLevelData;
        public SlowTurretData SlowTurretData => _turretsLevelsConfig.SlowTurretData;
        public RocketTurretLevelData RocketTurretLevelData => _turretsLevelsConfig.RocketTurretLevelData;
        public FlameTurretLevelData FlameTurretLevelData => _turretsLevelsConfig.FlameTurretLevelData;

        [Inject]
        public TurretsStatsProvider(TurretsLevelsConfig turretsLevelsConfig)
        {
            _turretsLevelsConfig = turretsLevelsConfig;
        }
    }
}

