using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Services
{
    public interface ITurretsStatsProvider
    {
        public CannonLevelData CannonLevelData { get; }
        public MinigunLevelData MinigunLevelData { get; }
        public AntiAirLevelData AntiAirLevelData { get; }
        public SlowTurretData SlowTurretData { get; }
        public RocketTurretLevelData RocketTurretLevelData { get; }
        public FlameTurretLevelData FlameTurretLevelData { get; }
    }
}