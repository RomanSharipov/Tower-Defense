using System.Collections.Generic;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class MapGenerator : MonoBehaviour
    {
        [Header("Size settings")]
        [SerializeField, Range(1, 50)] private int _gridWidth = 16;
        [SerializeField, Range(1, 50)] private int _gridDepth = 9;

        [Header("Tiles settings")]
        [SerializeField] private float _offsetObstacleSpawn = 0.2f;
        [SerializeField] private Tile[] _emptyTilePrefabs;
        [SerializeField] private Tile[] _obstacleTilePrefabs;
        [SerializeField] private Transform _parent;
        [SerializeField] private float tileSpacing = 1.0f;
        [SerializeField, Range(0, 100)] private int obstacleSpawnChance = 20;

        [Header("Tiles storage")]
        [SerializeField] private List<Transform> _emptyTiles = new List<Transform>();
        [SerializeField] private List<Transform> _obstacleTiles = new List<Transform>();

        [ContextMenu("Generate")]
        public void Generate()
        {
            ClearBoard();
            CreateEmptyTiles();
            CreateObstacleTiles();
        }

        private void CreateObstacleTiles()
        {
            foreach (Transform tileTransform in _emptyTiles)
            {
                if (Random.Range(0, 100) < obstacleSpawnChance)
                {
                    Tile obstacleTile = Instantiate(_obstacleTilePrefabs[Random.Range(0, _obstacleTilePrefabs.Length)], _parent);
                    obstacleTile.transform.position = new Vector3(tileTransform.position.x, tileTransform.position.y + _offsetObstacleSpawn, tileTransform.position.z);
                    _obstacleTiles.Add(obstacleTile.transform);
                }
            }
        }

        private void CreateEmptyTiles()
        {
            for (int r = 0; r < _gridDepth; r++)
            {
                int rOffset = r / 2;
                for (int q = -rOffset; q < _gridWidth - rOffset; q++)
                {
                    Tile tile = Instantiate(_emptyTilePrefabs[Random.Range(0, _emptyTilePrefabs.Length)], _parent);
                    tile.transform.position = HexCalculator.ToWorldPosition(q, r, tileSpacing);
                    _emptyTiles.Add(tile.transform);
                }
            }
        }

        private void ClearBoard()
        {
            foreach (Transform item in _emptyTiles)
            {
                DestroyImmediate(item.gameObject);
            }

            foreach (Transform item in _obstacleTiles)
            {
                DestroyImmediate(item.gameObject);
            }
            _emptyTiles.Clear();
            _obstacleTiles.Clear();
        }
    }
}
