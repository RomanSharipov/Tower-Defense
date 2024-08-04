using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.UI.Services;
using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(fileName = "StaticData",menuName = "StaticData/MainStaticData")]
    public class MainStaticData : ScriptableObject
    {
        [Header("All Windows")]
        public WindowsData WindowsData;

        [Header("All Scenes")]
        public SceneReference[] SceneReferences;

        [Header("Tiles")]
        public TileIdAssetReference[] Tiles;
    }
}