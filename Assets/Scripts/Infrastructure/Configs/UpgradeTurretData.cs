using System;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeTurretData", menuName = "StaticData/UpgradeTurretData")]
public class UpgradeTurretData : ScriptableObject
{
    [SerializeField] private MinigunLevelData[] _minigunLevelData;
    [SerializeField] private CannonLevelData[] _cannonUpgradeData;
    [SerializeField] private SlowTurretData[] _slowTurretData;

    public MinigunLevelData[] MinigunLevelData => _minigunLevelData;
    public CannonLevelData[] CannonLevelData => _cannonUpgradeData;
    public SlowTurretData[] SlowTurretData => _slowTurretData;
}
[Serializable]
public class MinigunLevelData
{
    public int Damage;
    public int Price;
    public float DetectionRange;
}
[Serializable]
public class CannonLevelData
{
    public int Damage;
    public int Price;
    public float DetectionRange;
    public float IntervalBetweenShoot;
}

[Serializable]
public class SlowTurretData
{
    public int Damage;
    public int Price;
    public float DetectionRange;
    public float DurationSlowing;
    public float PercentSlowing;
}
