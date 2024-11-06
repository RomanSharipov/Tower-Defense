using Assets.Scripts.CoreGamePlay;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using VContainer;
using UniRx;

public class TestServices : MonoBehaviour
{
    [Inject] private IClickOnTurretTracker _clickOnTurretTracker;


    private void Awake()
    {

        _clickOnTurretTracker.ClickOnTurret
            .Subscribe(turret => Debug.Log($"turret = {turret}"))
            .AddTo(this);
        _clickOnTurretTracker.StartTracking();
    }
}
