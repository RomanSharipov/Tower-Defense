using Assets.Scripts.CoreGamePlay;
using Assets.Scripts.Infrastructure.Services;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.UI.Services;
using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(fileName = "StaticData",menuName = "StaticData/MainStaticData")]
    public class MainStaticData : ScriptableObject
    {
        
        public WindowsData WindowsData;

       
        public SceneReference[] SceneReferences;

       
        public TileIdAssetReference[] Tiles;

        
        public EnemyAssetReference[] Enemies;

        
        public TurretAssetReference[] Turrets;
    }
}