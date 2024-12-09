using Assets.Scripts.CoreGamePlay;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using VContainer;
using UniRx;

public class TestServices : MonoBehaviour
{
    [Inject] private TurretsStatsLevelIndexService TurretsStatsSaveService;
    [SerializeField] private StatsType StatsType;

    [ContextMenu("LevelUp()")]
    public void LevelUp()
    {
        TurretsStatsSaveService.LevelUpStat(StatsType);

    }
}
