using Assets.Scripts.CoreGamePlay;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.UI.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Configs
{
    [CreateAssetMenu(fileName = "AddressablesAssetReferencesData", menuName = "StaticData/AddressablesAssetReferencesData")]
    public class AddressablesAssetReferencesData : ScriptableObject
    {
        
        public WindowsData WindowsData;

       
        public SceneReference[] SceneReferences;


        public AssetReference[] LevelReferences;

       
        public TileIdAssetReference[] Tiles;

        
        public EnemyAssetReference[] Enemies;

        
        public TurretAssetReference[] Turrets;
    }
}