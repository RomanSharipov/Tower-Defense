using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretsLevelsConfig", menuName = "StaticData/TurretsLevelsConfig")]
public class TurretsLevelsConfig : ScriptableObject
{
    [SerializeField] private MinigunLevelData _minigunLevelData;
    [SerializeField] private CannonLevelData _cannonUpgradeData;
    [SerializeField] private SlowTurretData _slowTurretData;
    [SerializeField] private RocketTurretLevelData _rocketTurretLevelData;
    [SerializeField] private FlameTurretLevelData _flameTurretLevelData;
    [SerializeField] private AntiAirLevelData _antiAirLevelData;

    public AntiAirLevelData AntiAirLevelData => _antiAirLevelData;
    public CannonLevelData CannonLevelData => _cannonUpgradeData;
    public FlameTurretLevelData FlameTurretLevelData => _flameTurretLevelData;
    public MinigunLevelData MinigunLevelData => _minigunLevelData;
    public SlowTurretData SlowTurretData => _slowTurretData;
    public RocketTurretLevelData RocketTurretLevelData => _rocketTurretLevelData;
}

[Serializable]
public class AntiAirLevelData
{
    public DetectDistanceUpgrade[] DetectDistance;
    public DamageUpgrade[] DamageUpgrade;
    public ReloadTimeUpgrade[] ReloadTimeUpgrade;
}

[Serializable]
public class MinigunLevelData
{
    public DetectDistanceUpgrade[] DetectDistance;
    public DamageUpgrade[] DamageUpgrade;
}
[Serializable]
public class CannonLevelData
{
    public DetectDistanceUpgrade[] DetectDistance;
    public DamageUpgrade[] DamageUpgrade;
    public ReloadTimeUpgrade[] ReloadTimeUpgrade;
}

[Serializable]
public class SlowTurretData
{
    public DetectDistanceUpgrade[] DetectDistance;
    public DamageUpgrade[] DamageUpgrade;
    public SlowDurationUpgrade[] SlowDurationUpgrade;
    public SlowPercentUpgrade[] SlowPercentUpgrade;
}

[Serializable]
public class FlameTurretLevelData
{
    public DetectDistanceUpgrade[] DetectDistance;
    public DamageUpgrade[] DamageUpgrade;
}
[Serializable]
public class RocketTurretLevelData
{
    public DetectDistanceUpgrade[] DetectDistance;
    public DamageUpgrade[] DamageUpgrade;
    public ReloadTimeUpgrade[] ReloadTimeUpgrade;
}
