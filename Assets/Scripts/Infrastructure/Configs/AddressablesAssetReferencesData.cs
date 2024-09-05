using Assets.Scripts.CoreGamePlay;
using Assets.Scripts.Infrastructure.Services;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.UI.Services;
using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(fileName = "AddressablesAssetReferencesData", menuName = "StaticData/AddressablesAssetReferencesData")]
    public class AddressablesAssetReferencesData : ScriptableObject
    {
        
        public WindowsData WindowsData;

       
        public SceneReference[] SceneReferences;

       
        public TileIdAssetReference[] Tiles;

        
        public EnemyAssetReference[] Enemies;

        
        public TurretAssetReference[] Turrets;
    }
}