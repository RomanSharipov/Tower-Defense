using System;
using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Services
{
    public class TurretsStatsLevelIndexService : ITurretsStatsLevelIndexService, IInitializable
    {
        public string Filename = "TurretsStats";

        private ISaveService _saveService;

        private Dictionary<StatsType, int> _stats;
        private readonly IReadOnlyDictionary<StatsType, int> _statsLimits = new Dictionary<StatsType, int>();

        [Inject]
        public TurretsStatsLevelIndexService(ISaveService saveService)
        {
            _saveService = saveService;
        }

        public void LevelUpStat(StatsType statsType)
        {
            if (LevelIsMax(statsType))
                return;

            _stats[statsType]++;
            _saveService.Save(_stats, Filename);
        }

        public int GetCurrentValue(StatsType statsType)
        {
            return _stats[statsType];
        }

        public bool LevelIsMax(StatsType statsType)
        {
            if (_statsLimits.TryGetValue(statsType, out int limit))
            {
                if (_stats[statsType] >= limit)
                {
                    return true;
                }
            }
            return false;
        }

        public void Initialize()
        {
            if (_saveService.HasSaved(Filename))
            {
                _stats = _saveService.Load<Dictionary<StatsType, int>>(Filename);

            }
            else
            {
                _stats = new();

                foreach (StatsType item in Enum.GetValues(typeof(StatsType)))
                {
                    _stats[item] = 0;
                }
                _saveService.Save(_stats, Filename);

            }
        }
    }
}

