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
        private ITurretsStatsData _turretsStatsData;

        private Dictionary<StatsType, int> _stats;
        
        [Inject]
        public TurretsStatsLevelIndexService(ISaveService saveService, ITurretsStatsData turretsStatsData)
        {
            _saveService = saveService;
            _turretsStatsData = turretsStatsData;
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
            int currentLevel = GetCurrentValue(statsType) + 1;

            return statsType switch
            {
                StatsType.MinigunDamage => currentLevel >= _turretsStatsData.MinigunLevelData.Damage.Length,
                StatsType.MinigunAttackDistance => currentLevel >= _turretsStatsData.MinigunLevelData.DetectDistance.Length,
                StatsType.CannonDamage => currentLevel >= _turretsStatsData.CannonLevelData.Damage.Length,
                StatsType.CannonReloadTime => currentLevel >= _turretsStatsData.CannonLevelData.ReloadTime.Length,
                StatsType.CannonAttackDistance => currentLevel >= _turretsStatsData.CannonLevelData.DetectDistance.Length,
                StatsType.AntiAirDamage => currentLevel >= _turretsStatsData.AntiAirLevelData.Damage.Length,
                StatsType.AntiAirReloadTime => currentLevel >= _turretsStatsData.AntiAirLevelData.ReloadTime.Length,
                StatsType.AntiAirAttackDistance => currentLevel >= _turretsStatsData.AntiAirLevelData.DetectDistance.Length,
                StatsType.FlameDamage => currentLevel >= _turretsStatsData.FlameTurretLevelData.Damage.Length,
                StatsType.FlameAttackDistance => currentLevel >= _turretsStatsData.FlameTurretLevelData.DetectDistance.Length,
                StatsType.RocketDamage => currentLevel >= _turretsStatsData.RocketTurretLevelData.Damage.Length,
                StatsType.RocketReloadTime => currentLevel >= _turretsStatsData.RocketTurretLevelData.ReloadTime.Length,
                StatsType.RocketAttackDistance => currentLevel >= _turretsStatsData.MinigunLevelData.DetectDistance.Length,
                StatsType.SlowDamage => currentLevel >= _turretsStatsData.SlowTurretData.Damage.Length,
                StatsType.SlowAttackDistance => currentLevel >= _turretsStatsData.SlowTurretData.DetectDistance.Length,
                StatsType.SlowPercent => currentLevel >= _turretsStatsData.SlowTurretData.SlowPercent.Length,
                StatsType.SlowDuration => currentLevel >= _turretsStatsData.SlowTurretData.SlowDuration.Length,
                StatsType.None => throw new NotImplementedException(),
                _ => throw new NotImplementedException(),
            };
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

