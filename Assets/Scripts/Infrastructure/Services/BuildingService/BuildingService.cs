using Assets.Scripts.CoreGamePlay;
using Assets.Scripts.CoreGamePlay.Turrets;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.Infrastructure.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly ITurretFactory _turretFactory;
        private readonly Camera _camera;
        
        private readonly Dictionary<Collider, TileView> _tileViewCache = new Dictionary<Collider, TileView>();

        [Inject]
        public BuildingService(ITurretFactory turretFactory, Camera camera)
        {
            _turretFactory = turretFactory;
            _camera = camera;
        }

        public async UniTask StartBuilding()
        {
            TurretBase turretBase = await _turretFactory.CreateTurret<SimpleTurret>(TurretId.Simple);
            
            while (true)
            {
                bool cursorOnTile = TryGetTileUnderCursor(out TileView tile);

                if (Input.GetMouseButtonUp(0))
                {
                    if (cursorOnTile)
                    {
                        turretBase.transform.position = tile.transform.position;
                        tile.UpdateWalkable(TileId.Obstacle);
                    }
                    else
                    {
                        GameObject.Destroy(turretBase.gameObject);
                    }
                    break;
                }
                if (cursorOnTile)
                {
                    turretBase.transform.position = tile.transform.position;
                }
                await UniTask.Yield();
            }
        }

        private bool TryGetTileUnderCursor(out TileView tileView)
        {
            tileView = null;

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Collider hitCollider = hit.collider;
                if (_tileViewCache.TryGetValue(hitCollider, out tileView))
                {
                    return true;
                }
                tileView = hitCollider.GetComponent<TileView>();
                
                if (tileView != null)
                {
                    _tileViewCache[hitCollider] = tileView;
                    return true;
                }
            }

            return false;
        }
    }
}
