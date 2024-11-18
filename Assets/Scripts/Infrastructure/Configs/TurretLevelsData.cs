using System;

[Serializable]
public class ReloadTimeUpgrade
{
    public DetectDistanceUpgrade[] DetectDistance;
    public DamageUpgrade[] DamageUpgrade;
}

[Serializable]
public class DetectDistanceUpgrade
{
    public float DetectionRadius;
    public int Price;
}

[Serializable]
public class DamageUpgrade
{
    public int Damage;
    public int Price;
}

[Serializable]
public class SlowDurationUpgrade
{
    public float Duration;
    public int Price;
}

[Serializable]
public class SlowPercentUpgrade
{
    public float Percent;
    public int Price;
}


