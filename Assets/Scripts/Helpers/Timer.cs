using UnityEngine;

public class Timer
{
    private float _lastActionTime;
    private float _timeBetweenAction;
    
    public Timer(float timeBetweenAction)
    {
        _timeBetweenAction = timeBetweenAction;
    }
    
    public bool IsActionTimeReached()
    {
        if (_lastActionTime <= 0)
        {
            _lastActionTime = _timeBetweenAction;
            return true;
        }
        _lastActionTime -= Time.deltaTime;
        return false;
    }
}