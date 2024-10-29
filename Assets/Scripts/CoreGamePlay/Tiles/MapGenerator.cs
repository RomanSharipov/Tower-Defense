using System.Collections;
using System.Collections.Generic;
using CodeBase.Helpers;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private LevelMainMonoBehaviour _levelMainMonoBehaviour;

        [Header("Form game board settings")]
        [Header("Rect GameBoard")]
        [SerializeField, Range(1, 50)] private int _gridWidth = 16;
        [SerializeField, Range(1, 50)] private int _gridDepth = 9;

        [SerializeField] private float _offsetObstacleSpawn = 0.2f;
        [SerializeField] private TileView[] _emptyTilePrefabs;
        [SerializeField] private TileView[] _obstacleTilePrefabs;
        [SerializeField] private Transform _parent;
        [SerializeField] private float tileSpacing = 1.7f;
        [SerializeField, Range(0, 100)] private int obstacleSpawnChance = 20;

        [SerializeField, Range(0.1f, 10f)] private float roadWidth = 1f;

        [Header("Tiles storage")]
        [SerializeField] private List<TileView> _gameBoardTiles = new List<TileView>();
        [SerializeField] private List<Transform> _obstacleTiles = new List<Transform>();

        [SerializeField] private Transform[] _points;
        [SerializeField] private bool _createRoad;
        
        [ContextMenu("Generate")]
        public void Generate()
        {
            ClearBoard();
            CreateRectGameBoard();
            if (_createRoad)
            {
                CreateRoad();
            }
            CreateObstacleTiles();
            _levelMainMonoBehaviour.SetTiles(_gameBoardTiles.ToArray());
        }
        [ContextMenu("UpdateTilesInLevelMainMonoBehaviour")]
        private void UpdateTiles()
        {
#if UNITY_EDITOR
            _gameBoardTiles.RemoveAll(item => item == null);
            _levelMainMonoBehaviour.SetTiles(_gameBoardTiles.ToArray());
            UnityEditor.SceneManagement.EditorSceneManager.SaveScene(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());
#endif
        }

        private void CreateRoad()
        {
            List<Vector3> bezierPoints = CalculateBezierCurve(_points, 20); // 20 points along the curve

            List<TileView> roadTiles = new List<TileView>();

            foreach (Vector3 bezierPoint in bezierPoints)
            {
                foreach (TileView tileTransform in _gameBoardTiles)
                {
                    if (Vector3.Distance(tileTransform.transform.position, bezierPoint) <= roadWidth)
                    {
                        if (!roadTiles.Contains(tileTransform))
                        {
                            roadTiles.Add(tileTransform);
                        }
                    }
                }
            }

            List<TileView> tilesToRemove = new List<TileView>(_gameBoardTiles);

            foreach (TileView roadTile in roadTiles)
            {
                tilesToRemove.Remove(roadTile);
            }

            foreach (TileView tile in tilesToRemove)
            {
                DestroyImmediate(tile.gameObject);
            }

            _gameBoardTiles = roadTiles;
        }

        private List<Vector3> CalculateBezierCurve(Transform[] points, int segmentCount)
        {
            List<Vector3> bezierCurve = new List<Vector3>();

            for (int i = 0; i <= segmentCount; i++)
            {
                float t = i / (float)segmentCount;
                Vector3 pointOnCurve = CalculateBezierPoint(t, points);
                bezierCurve.Add(pointOnCurve);
            }

            return bezierCurve;
        }

        private Vector3 CalculateBezierPoint(float t, Transform[] points)
        {
            int n = points.Length - 1;
            Vector3 point = Vector3.zero;

            for (int i = 0; i <= n; i++)
            {
                point += BinomialCoefficient(n, i) * Mathf.Pow(1 - t, n - i) * Mathf.Pow(t, i) * points[i].position;
            }

            return point;
        }

        private int BinomialCoefficient(int n, int k)
        {
            int result = 1;
            for (int i = 1; i <= k; i++)
            {
                result *= (n - (k - i));
                result /= i;
            }
            return result;
        }

        private void CreateRectGameBoard()
        {
            int i = 0;

            for (int r = 0; r < _gridDepth; r++)
            {
                int rOffset = r / 2;
                for (int q = -rOffset; q < _gridWidth - rOffset; q++)
                {
                    TileView tile = Instantiate(_emptyTilePrefabs[Random.Range(0, _emptyTilePrefabs.Length)], _parent);
                    tile.Construct(q,r);
                    tile.transform.position = HexCalculator.ToWorldPosition(q, r, tileSpacing);
                    i++;
                    tile.name = $"{i}.TileView";
                    _gameBoardTiles.Add(tile);
                }
            }
        }

        [ContextMenu("ClearBoard")]
        private void ClearBoard()
        {
            foreach (TileView item in _gameBoardTiles)
            {
                DestroyImmediate(item.gameObject);
            }

            foreach (Transform item in _obstacleTiles)
            {
                DestroyImmediate(item.gameObject);
            }
            _gameBoardTiles.Clear();
            _obstacleTiles.Clear();
        }

        private void CreateObstacleTiles()
        {
            foreach (TileView tileTransform in _gameBoardTiles)
            {
                if (Random.Range(0, 100) < obstacleSpawnChance)
                {
                    TileView obstacleTile = Instantiate(_obstacleTilePrefabs[Random.Range(0, _obstacleTilePrefabs.Length)], _parent);
                    obstacleTile.transform.position = new Vector3(tileTransform.transform.position.x, tileTransform.transform.position.y + _offsetObstacleSpawn, tileTransform.transform.position.z);
                    _obstacleTiles.Add(obstacleTile.transform);
                    tileTransform.SetWalkable(TileId.Obstacle);
                }
            }
        }
    }
}
