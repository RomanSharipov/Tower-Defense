using System;
using Assets.Scripts.CoreGamePlay;
using BezierSolution;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using VContainer;

public class PathInitializer : MonoBehaviour
{
    [Inject] private ICacherOfPath _cacherOfPath;
    [Inject] private IBuildingService _buildingService;
    [Inject] private ITurretRemover _turretRemover;

    [SerializeField] private PathFlyData[] _pathFlyDatas;
    
    public void Init()
    {
        _buildingService.TurretIsBuilded += OnTurretIsBuilded;
        _turretRemover.TurretRemoved += OnTurretRemove;
        UpdateTilePath();

        foreach (PathFlyData pathFlyData in _pathFlyDatas)
        {
            _cacherOfPath.RegisterFlyPath(pathFlyData.EnemySpawner, pathFlyData.PathFly);
        }
    }

    private void OnTurretRemove(TurretBase turret)
    {
        UpdateTilePath();
    }

    private void OnTurretIsBuilded(TurretBase turret)
    {
        UpdateTilePath();
    }

    private void OnDisable()
    {
        _buildingService.TurretIsBuilded -= OnTurretIsBuilded;
        _turretRemover.TurretRemoved -= OnTurretRemove;
    }

    private void UpdateTilePath()
    {
        _cacherOfPath.TryBuildPath();
        _cacherOfPath.SetNewPath();
    }
}

[Serializable]
public class PathFlyData
{
    public EnemySpawner EnemySpawner;
    public BezierSpline PathFly;
}
