using System;
using System.Collections.Generic;
using Assets.Scripts.CoreGamePlay;
using Tarodev_Pathfinding._Scripts;
using UnityEngine;

[Serializable]
public class PathBuilder
{
    private TileData[] _path;
    [SerializeField] private TileView[] _pathView;
    [SerializeField] private bool _newPathOppositeDirection;
    [SerializeField] private bool _buildedRightInFrontOfUs;
    private HashSet<TileData> _remaingsPath = new HashSet<TileData>();
    public bool NewPathOppositeDirection => _newPathOppositeDirection;
    public bool BuildedRightInFrontOfUs => _buildedRightInFrontOfUs;
    public TileData[] Path => _path;

    public bool TryUpdatePath(TileData newObstacleTile,int currentTargetIndex)
    {
        if (_remaingsPath.Contains(newObstacleTile))
        {
            TileData currentTarget = _path[currentTargetIndex];
            List<TileData> newListPath = Pathfinding.FindPath(currentTarget, _path[_path.Length - 1]);

            TileData previousTileData = null;
            TileData firstTileNewPath = newListPath[0];

            if (currentTargetIndex > 0)
            {
                previousTileData = _path[currentTargetIndex - 1];
            }

            _buildedRightInFrontOfUs = newObstacleTile == currentTarget;
            _newPathOppositeDirection = previousTileData == firstTileNewPath;

            CopyToView();

            newListPath.Add(currentTarget);
            newListPath.Reverse();
            SetPath(newListPath.ToArray());
            return true;
        }
        return false;
    }

    

    void CopyToView()
    {
        _pathView = new TileView[_path.Length];
        for (int i = 0; i < _path.Length; i++)
        {
            _pathView[i] = _path[i].Tile;
        }
    }

    public void SetPath(TileData[] pathPoints)
    {
        _path = pathPoints;
        CopyToView();
        _remaingsPath.Clear();
        foreach (TileData pathPoint in _path)
        {
            _remaingsPath.Add(pathPoint);
        }
        //_currentTargetIndex = 0;                          Потом перекинуть это в EnemyMovement
        //_currentTarget = _path[_currentTargetIndex];      Потом перекинуть это в EnemyMovement
    }

    public void RemoveCompletedTile(TileData completedTile)
    {
        _remaingsPath.Remove(completedTile);
    }
}