using Assets.Scripts.CoreGamePlay;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly ITurretFactory _turretFactory;
        private readonly ICacherOfPath _cacherOfPath;
        private readonly Camera _camera;

        private readonly Dictionary<Collider, TileView> _tileViewCache = new Dictionary<Collider, TileView>();
        private float _lastValidDistance = 0f;

        public event Action<TurretBase,TileData> TurretIsBuilded;


        [Inject]
        public BuildingService(ITurretFactory turretFactory, Camera camera, ICacherOfPath cacherOfPath)
        {
            _turretFactory = turretFactory;
            _camera = camera;
            _cacherOfPath = cacherOfPath;
        }

        public async UniTask StartBuilding(TurretId turretId)
        {
            TurretBase turretBase = await _turretFactory.CreateTurret(turretId);
            Ray ray;

            while (true)
            {
                ray = _camera.ScreenPointToRay(Input.mousePosition);
                bool cursorOnFreeTile = TryGetFreeTileUnderCursor(ray, out TileView tile);
                bool cursorOnEnemy = EnemyUnderCursor(ray);

                if (Input.GetMouseButtonUp(0))
                {
                    if (cursorOnFreeTile && _cacherOfPath.PathsIsExist)
                    {
                        turretBase.transform.position = tile.transform.position;
                        tile.SetWalkable(TileId.Obstacle);
                        tile.UpdateTileData();
                        turretBase.Init();
                        TurretIsBuilded?.Invoke(turretBase, tile.NodeBase);
                    }
                    else
                    {
                        GameObject.Destroy(turretBase.gameObject);
                    }
                    break;
                }

                if (cursorOnFreeTile && !cursorOnEnemy)
                {
                    turretBase.transform.position = tile.transform.position;
                    tile.NodeBase.SetWalkable(false);
                    UpdateTurretColor(turretBase);
                    tile.NodeBase.SetWalkable(true);
                }
                else
                {
                    turretBase.transform.position = ray.GetPoint(_lastValidDistance);
                    turretBase.SetColor(ColorType.BlockBuildColor);
                }

                await UniTask.Yield();
            }
        }

        private void UpdateTurretColor(TurretBase turretBase)
        {
            if (_cacherOfPath.TryBuildPath())
            {
                turretBase.SetColor(ColorType.TransparentColor);
            }
            else
            {
                turretBase.SetColor(ColorType.BlockBuildColor);
            }
        }

        private bool TryGetFreeTileUnderCursor(Ray ray, out TileView tileView)
        {
            tileView = null;

            if (Physics.Raycast(ray, out RaycastHit hitInternal))
            {
                Collider hitCollider = hitInternal.collider;
                _lastValidDistance = hitInternal.distance;
                if (_tileViewCache.TryGetValue(hitCollider, out tileView))
                {
                    if (tileView.NodeBase.Walkable)
                    {
                        return true;
                    }
                }
                tileView = hitCollider.GetComponent<TileView>();

                if (tileView != null)
                {
                    _tileViewCache[hitCollider] = tileView;
                    
                    if (tileView.NodeBase.Walkable)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool EnemyUnderCursor(Ray ray)
        {
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                
                if (hitObject.layer == LayerMask.NameToLayer("BlockBuilding"))
                {
                    return true; 
                }
            }
            return false;
        }
    }
}
