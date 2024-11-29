using System;

[Serializable]
public class ReloadTimeUpgrade
{
    public float IntervalBeetweenAttack;
    public float Price;
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
    public int Percent;
    public int Price;
}


