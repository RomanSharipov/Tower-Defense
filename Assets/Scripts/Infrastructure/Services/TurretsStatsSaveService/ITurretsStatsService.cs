public interface ITurretsStatsService
{
    public int GetCurrentValue(StatsType statsType);
    public bool LevelIsMax(StatsType statsType);
    public void LevelUpStat(StatsType statsType);
}