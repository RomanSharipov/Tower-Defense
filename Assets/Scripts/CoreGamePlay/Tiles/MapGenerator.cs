using System.Collections.Generic;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class MapGenerator : MonoBehaviour
    {
        [Header("Form game board settings")]
        [Header("Rect GameBoard")]
        [SerializeField, Range(1, 50)] private int _gridWidth = 16;
        [SerializeField, Range(1, 50)] private int _gridDepth = 9;

        [Header("Curved Road GameBoard")]
        [SerializeField, Range(1, 10)] private int roadThickness = 3;
        [SerializeField, Range(1, 100)] private int roadLength = 30;
        [SerializeField, Range(0, 10)] private float roadAmplitude = 3.0f;

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
            //CreateRectGameBoard();
            CreateCurvedRoadGameBoard();


            CreateObstacleTiles();
        }

        private void CreateCurvedRoadGameBoard()
        {
            for (int i = 0; i < roadLength; i++)
            {
                float t = i / (float)roadLength;
                float x = i;
                float y = Mathf.Sin(t * Mathf.PI * 2) * roadAmplitude;

                for (int j = -roadThickness / 2; j <= roadThickness / 2; j++)
                {
                    Tile tile = Instantiate(_emptyTilePrefabs[Random.Range(0, _emptyTilePrefabs.Length)], _parent);
                    tile.transform.position = HexCalculator.ToWorldPosition((int)x, (int)y + j, tileSpacing);
                    _emptyTiles.Add(tile.transform);
                }
            }
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

        private void CreateRectGameBoard()
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
