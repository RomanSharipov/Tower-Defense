using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class NodeBase
{
    public HexCoords Coords;
    public float GetDistance(NodeBase other) => Coords.GetDistance(other.Coords); // Helper to reduce noise in pathfinding
    public bool Walkable { get; private set; }
    private bool _selected;
    private Color _defaultColor;

    public virtual void Init(bool walkable, HexCoords coords)
    {
        Walkable = walkable;
        Coords = coords;
    }

    public static event Action<NodeBase> OnHoverTile;
    private void OnEnable() => OnHoverTile += OnOnHoverTile;
    private void OnDisable() => OnHoverTile -= OnOnHoverTile;
    private void OnOnHoverTile(NodeBase selected) => _selected = selected == this;

    protected virtual void OnMouseDown()
    {
        if (!Walkable) return;
        OnHoverTile?.Invoke(this);
    }



    [Header("Pathfinding")]
    [SerializeField]
    private TextMeshPro _fCostText;

    [SerializeField] private TextMeshPro _gCostText, _hCostText;
    public List<NodeBase> Neighbors { get; protected set; }
    public NodeBase Connection { get; private set; }
    public float G { get; private set; }
    public float H { get; private set; }
    public float F => G + H;

    public void CacheNeighbors()
    {
        Neighbors = GridManager.Instance.Tiles.Where(t => Coords.GetDistance(t.Value.Coords) == 1).Select(t => t.Value).ToList();
    }

    public void SetConnection(NodeBase nodeBase)
    {
        Connection = nodeBase;
    }

    public void SetG(float g)
    {
        G = g;
        SetText();
    }

    public void SetH(float h)
    {
        H = h;
        SetText();
    }

    private void SetText()
    {
        if (_selected) return;
        _gCostText.text = G.ToString();
        _hCostText.text = H.ToString();
        _fCostText.text = F.ToString();
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