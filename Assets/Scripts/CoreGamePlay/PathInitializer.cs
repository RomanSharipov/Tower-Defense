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

    [SerializeField] private PathFlyData[] _pathFlyDatas;
    
    public void Init()
    {
        _buildingService.TurretIsBuilded += OnTurretIsBuilded;
        UpdateTilePath();

        foreach (PathFlyData pathFlyData in _pathFlyDatas)
        {
            _cacherOfPath.RegisterFlyPath(pathFlyData.EnemySpawner, pathFlyData.PathFly);
        }
    }
    
    private void OnTurretIsBuilded(TurretBase turret, TileData data)
    {
        UpdateTilePath();
    }

    private void OnDisable()
    {
        _buildingService.TurretIsBuilded -= OnTurretIsBuilded;
    }

    private void UpdateTilePath()
    {
        _cacherOfPath.TryBuildPath();
    }
}

[Serializable]
public class PathFlyData
{
    public EnemySpawner EnemySpawner;
    public BezierSpline PathFly;
}
