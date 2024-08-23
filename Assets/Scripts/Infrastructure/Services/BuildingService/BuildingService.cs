using Assets.Scripts.CoreGamePlay;
using Assets.Scripts.CoreGamePlay.Turrets;
using Cysharp.Threading.Tasks;
using System;
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
        private float _lastValidDistance = 0f;

        public event Action<TurretBase,TileData> TurretIsBuilded;


        [Inject]
        public BuildingService(ITurretFactory turretFactory, Camera camera)
        {
            _turretFactory = turretFactory;
            _camera = camera;
        }

        public async UniTask StartBuilding()
        {
            TurretBase turretBase = await _turretFactory.CreateTurret<SimpleTurret>(TurretId.Simple);
            Ray ray;

            while (true)
            {
                ray = _camera.ScreenPointToRay(Input.mousePosition);
                bool cursorOnTile = TryGetTileUnderCursor(ray, out TileView tile);
                bool cursorOnEnemy = EnemyUnderCursor(ray);

                if (Input.GetMouseButtonUp(0))
                {
                    if (cursorOnTile)
                    {
                        turretBase.transform.position = tile.transform.position;
                        tile.UpdateWalkable(TileId.Obstacle);
                        TurretIsBuilded?.Invoke(turretBase, tile.NodeBase);
                    }
                    else
                    {
                        GameObject.Destroy(turretBase.gameObject);
                    }
                    break;
                }

                if (cursorOnTile && !cursorOnEnemy)
                {
                    turretBase.transform.position = tile.transform.position;
                }
                else
                {
                    turretBase.transform.position = ray.GetPoint(_lastValidDistance);
                }

                await UniTask.Yield();
            }
        }

        private bool TryGetTileUnderCursor(Ray ray, out TileView tileView)
        {
            tileView = null;

            if (Physics.Raycast(ray, out RaycastHit hitInternal))
            {
                Collider hitCollider = hitInternal.collider;
                _lastValidDistance = hitInternal.distance;
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

        private bool EnemyUnderCursor(Ray ray)
        {
            RaycastHit hit;

            
            if (Physics.Raycast(ray, out hit))
            {
                // Получаем объект, на который указывает луч
                GameObject hitObject = hit.collider.gameObject;

                // Проверяем, находится ли объект на слое "Enemy"
                if (hitObject.layer == LayerMask.NameToLayer("BlockBuilding"))
                {
                    return true; // Луч попал на врага
                }
            }

            // Если луч не попал на врага, возвращаем false
            return false;
        }
    }
}
