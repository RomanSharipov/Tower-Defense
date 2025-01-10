using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretsPriceConfig", menuName = "StaticData/TurretsPriceConfig")]
public class TurretsPriceConfig : ScriptableObject
{
    [SerializeField] private int _minigun;
    [SerializeField] private int _cannon;
    [SerializeField] private int _slow;
    [SerializeField] private int _antiAir;
    [SerializeField] private int _rocket;
    [SerializeField] private int _flameTurret;
    
    public int Minigun => _minigun;
    public int FlameTurret => _flameTurret;
    public int Cannon => _cannon;
    public int Slow => _slow;
    public int Rocket => _rocket;
    public int AntiAir => _antiAir;

    [SerializeField] private int[] _minigunUpgradePrices;
    [SerializeField] private int[] _cannonUpgradePrices;
    [SerializeField] private int[] _slowUpgradePrices;
    [SerializeField] private int[] _antiAirUpgradePrices;
    [SerializeField] private int[] _rocketUpgradePrices;
    [SerializeField] private int[] _flameTurretUpgradePrices;

    public int[] MinigunUpgradePrices => _minigunUpgradePrices;
    public int[] FlameTurretUpgradePrices => _flameTurretUpgradePrices;
    public int[] CannonUpgradePrices => _cannonUpgradePrices;
    public int[] SlowUpgradePrices => _slowUpgradePrices;
    public int[] RocketUpgradePrices => _rocketUpgradePrices;
    public int[] AntiAirUpgradePrices => _antiAirUpgradePrices;
}


