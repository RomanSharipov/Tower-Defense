using CodeBase.Infrastructure.Services;

public interface ITurretsStatsLevelIndexService
{
    public int GetCurrentValue(StatsType statsType);
    public bool LevelIsMax(StatsType statsType);
    public void LevelUpStat(StatsType statsType);
}