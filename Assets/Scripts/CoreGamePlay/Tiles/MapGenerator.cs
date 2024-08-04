using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField, Range(1, 50)] private int _gridWidth = 16;
        [SerializeField, Range(1, 50)] private int _gridDepth = 9;

        [SerializeField] private Tile _tilePrefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private float tileSpacing = 1.0f;

        [ContextMenu("Generate")]
        public void Generate()
        {
            for (int r = 0; r < _gridDepth; r++)
            {
                int rOffset = r / 2;
                for (int q = -rOffset; q < _gridWidth - rOffset; q++)
                {
                    Tile tile = Instantiate(_tilePrefab, _parent);
                    tile.transform.position = HexCalculator.ToWorldPosition(q, r, tileSpacing);
                }
            }
        }
    }
}
