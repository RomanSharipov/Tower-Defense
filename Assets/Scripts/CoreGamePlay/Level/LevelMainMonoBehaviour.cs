using System.Linq;
using UnityEngine;
using VContainer;
using CodeBase.Infrastructure.Services;

namespace Assets.Scripts.CoreGamePlay
{
    public class LevelMainMonoBehaviour : MonoBehaviour, ILevelMain
    {
        [Inject] private ITurretFactory _turretFactory;
        [Inject] private IBuildingService _buildingService;
        [Inject] private ICacherOfPath _cacherOfPath;
        [Inject] private ITilesStorage _tilesStorage;

        [SerializeField] private TileView[] _tiles;
        [SerializeField] private Transform _turretsParrent;
        [SerializeField] private EnemySpawner[] _enemySpawners;
        [SerializeField] private PathInitializer _pathInitializer;

        
        private void OnEnable()
        {
            _buildingService.TurretIsBuilded += OnTurretIsBuilded;
        }

        private void OnDisable()
        {
            _buildingService.TurretIsBuilded -= OnTurretIsBuilded;
        }

        private void OnTurretIsBuilded(TurretBase turret, TileData tileData)
        {

        }

        public void InitializeSceneServices()
        {
            _cacherOfPath.SetSpawnersOnCurrentLevel(_enemySpawners);
            InitGameBoard();

            _turretFactory.SetParrentTurret(_turretsParrent);
            _pathInitializer.Init();
            StartSpawnEnemies();
        }
        
        public void StartSpawnEnemies()
        {
            foreach (EnemySpawner enemySpawner in _enemySpawners)
            {
                enemySpawner.StartSpawnEnemies();
            }
        }

        private void InitGameBoard()
        {
            TileData[] tilesData = new TileData[_tiles.Length];

            for (int i = 0; i < _tiles.Length; i++)
            {
                tilesData[i] = _tiles[i].NodeBase;
            }
            _tilesStorage.SetTiles(tilesData);

            foreach (TileView tile in _tiles)
            {
                tile.InitializeNode(_tiles.ToList());
            }

            foreach (TileView tile in _tiles)
            {
                tile.NodeBase.CacheNeighbors();
            }
        }

        public void SetTiles(TileView[] tiles)
        {
            _tiles = tiles;
        }
    }
}
