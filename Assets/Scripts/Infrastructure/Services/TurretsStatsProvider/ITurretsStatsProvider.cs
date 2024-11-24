using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Services
{
    public interface ITurretsStatsProvider
    {
        CannonLevelData CannonLevelData { get; }
    }
}