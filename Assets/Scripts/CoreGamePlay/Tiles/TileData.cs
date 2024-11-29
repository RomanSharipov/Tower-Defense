using System;
using System.Collections.Generic;
using Assets.Scripts.CoreGamePlay;
using UnityEngine;

public class TileData
{
    public HexCoords Coords;
    public float GetDistance(TileData other) => Coords.GetDistance(other.Coords); // Helper to reduce noise in pathfinding
    public bool Walkable { get; private set; }
    public bool TileIsFree { get; private set; }

    private List<TileView> _gameBoardTiles;
    private TileView _tile;

    public TileView Tile => _tile;

    public TileData(bool walkable, HexCoords coords, List<TileView> gameBoardTiles, TileView tile)
    {
        Walkable = walkable;
        Coords = coords;
        _gameBoardTiles = gameBoardTiles;
        _tile = tile;
    }
    
    public List<TileData> Neighbors { get; protected set; }
    public TileData Connection { get; private set; }
    public float G { get; private set; }
    public float H { get; private set; }
    public float F => G + H;

    public void SetWalkable(bool walkable)
    {
        Walkable = walkable;
    }

    public void SetIsFreeStatus(bool tileIsFree)
    {
        TileIsFree = tileIsFree;
    }

    public void CacheNeighbors()
    {
        Neighbors = new List<TileData>();

        foreach (TileView tile in _gameBoardTiles)
        {
            TileData currentTile = tile.NodeBase;
            if (Coords.GetDistance(currentTile.Coords) == 1)
            {
                Neighbors.Add(currentTile);
            }
        }
    }

    public void SetConnection(TileData nodeBase)
    {
        Connection = nodeBase;
    }

    public void SetG(float g)
    {
        G = g;
    }

    public void SetH(float h)
    {
        H = h;
    }
}

[Serializable]
public struct HexCoords
{
    private readonly int _q;
    private readonly int _r;

    public HexCoords(int q, int r)
    {
        _q = q;
        _r = r;
        Pos = _q * new Vector2(Sqrt3, 0) + _r * new Vector2(Sqrt3 / 2, 1.5f);
    }

    public float GetDistance(HexCoords other) => (this - other).AxialLength();

    private static readonly float Sqrt3 = Mathf.Sqrt(3);

    public Vector2 Pos { get; set; }
    public int Q => _q;
    public int R => _r;

    private int AxialLength()
    {
        if (_q == 0 && _r == 0) return 0;
        if (_q > 0 && _r >= 0) return _q + _r;
        if (_q <= 0 && _r > 0) return -_q < _r ? _r : -_q;
        if (_q < 0) return -_q - _r;
        return -_r > _q ? -_r : _q;
    }

    public static HexCoords operator -(HexCoords a, HexCoords b)
    {
        return new HexCoords(a._q - b._q, a._r - b._r);
    }
}