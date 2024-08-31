using System;
using System.Collections.Generic;
using Assets.Scripts.CoreGamePlay;
using Tarodev_Pathfinding._Scripts;
using UnityEngine;

[Serializable]
public class PathBuilder
{
    private List<TileData> _path;
    [SerializeField] private TileView[] _pathView;

    private HashSet<TileData> _remaingsPath = new HashSet<TileData>();

    public List<TileData> Path => _path;


    public bool TryUpdatePath(TileData newObstacleTile, int currentTargetIndex, out bool buildedRightInFrontOfUs, out bool newPathOppositeDirection)
    {
        if (_remaingsPath.Contains(newObstacleTile))
        {
            TileData currentTarget = _path[currentTargetIndex];
            List<TileData> newListPath = Pathfinding.FindPath(currentTarget, _path[_path.Count - 1]);

            TileData previousTileData = null;
            TileData firstTileNewPath = newListPath[newListPath.Count - 1];

            if (currentTargetIndex > 0)
            {
                previousTileData = _path[currentTargetIndex - 1];
            }

            buildedRightInFrontOfUs = newObstacleTile == currentTarget;
            newPathOppositeDirection = previousTileData == firstTileNewPath;

            CopyToView();

            newListPath.Add(currentTarget);
            newListPath.Reverse();
            SetPath(newListPath);
            return true;
        }
        buildedRightInFrontOfUs = default;
        newPathOppositeDirection = default;
        return false;
    }

    public void UpdatePath(TileData currentTarget)
    {
        List<TileData> newListPath = Pathfinding.FindPath(currentTarget, _path[_path.Count - 1]);
        newListPath.Add(currentTarget);
        newListPath.Reverse();
        SetPath(newListPath);
    }
    
    void CopyToView()
    {
        _pathView = new TileView[_path.Count];
        for (int i = 0; i < _path.Count; i++)
        {
            _pathView[i] = _path[i].Tile;
        }
    }

    public void SetPath(List<TileData> pathPoints)
    {
        _path = pathPoints;
        CopyToView();
        _remaingsPath.Clear();
        foreach (TileData pathPoint in _path)
        {
            _remaingsPath.Add(pathPoint);
        }
    }

    public void RemoveCompletedTile(TileData completedTile)
    {
        _remaingsPath.Remove(completedTile);
    }
}