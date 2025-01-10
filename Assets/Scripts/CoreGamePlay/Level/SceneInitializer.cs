using System.Linq;
using UnityEngine;
using VContainer;
using CodeBase.Infrastructure.Services;
using CodeBase.Configs;

namespace Assets.Scripts.CoreGamePlay
{
    public class SceneInitializer : MonoBehaviour, ISceneInitializer
    {
        [Inject] private ITurretFactory _turretFactory;
        [Inject] private ICacherOfPath _cacherOfPath;
        [Inject] private ITilesStorage _tilesStorage;
        [Inject] private IWavesService _wavesService;
        [Inject] private IPlayerHealthService _playerHealthService;
        [Inject] private IPlayerResourcesService _playerResourcesService;
        [Inject] private IAllEnemyStorage _allEnemyStorage;

        [SerializeField] private TileView[] _tiles;
        [SerializeField] private Transform _turretsParrent;
        [SerializeField] private EnemySpawner[] _enemySpawners;
        [SerializeField] private PathInitializer _pathInitializer;
        [SerializeField] private WavesOnLevelData _wavesOnLevelData;
        [SerializeField] private int _levelHealth = 10;
        [SerializeField] private int _startMoney = 20;

        public int LevelHealth => _levelHealth;

        public void InitializeSceneServices()
        {
            _playerHealthService.Init(_levelHealth);
            _playerResourcesService.SetValue(ResourcesType.Money, _startMoney);
            _cacherOfPath.SetSpawnersOnCurrentLevel(_enemySpawners);
            InitHexTiles();

            _turretFactory.SetParrentTurret(_turretsParrent);
            _pathInitializer.Init();
            _wavesService.Initialize(_wavesOnLevelData);
            InitSpawners();
        }

        private void InitSpawners()
        {
            foreach (EnemySpawner enemySpawner in _enemySpawners)
            {
                enemySpawner.Init();
            }
        }

        private void InitHexTiles()
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
