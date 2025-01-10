using Assets.Scripts.CoreGamePlay;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using VContainer;
using UniRx;

public class TestServices : MonoBehaviour
{
    [Inject] private ITurretsStatsLevelIndexService TurretsStatsSaveService;
    [Inject] private IWavesService WavesService;
    [SerializeField] private StatsType StatsType;

    [ContextMenu("LevelUp()")]
    public void LevelUp()
    {
        TurretsStatsSaveService.LevelUpStat(StatsType);

    }
    [ContextMenu("WavesService.AllWavesIsOver()")]
    public void WavesServiceAllWavesIsOver()
    {
        Debug.Log($"WavesService.AllWavesIsOver()= {WavesService.AllWavesIsOver()}");

    }
}
