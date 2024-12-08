using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class TurretsStatsService : ITurretsStatsService ,IInitializable
{
    public string Filename = "TurretsStats";


    private ISaveService _saveService;

    
    private Dictionary<StatsType, int> _stats;
    private readonly IReadOnlyDictionary<StatsType, int> _statsLimits = new Dictionary<StatsType, int>();
    
    [Inject]
    public TurretsStatsService(ISaveService saveService)
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

public enum StatsType 
{
    None = 0,
    MinigunDamage = 1,
    CannonDamage = 2,
    CannonReloadTime = 3,
}
