using System.Collections.Generic;
using CodeBase.Configs;
using CodeBase.Infrastructure.UI.Services;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services
{
    public class StaticDataService : IStaticDataService
    {
        private readonly MainStaticData _mainStaticData;

        public IReadOnlyDictionary<WindowId, AssetReference> Windows => 
            _mainStaticData.WindowsData.WindowAssetReference.ToDictionary<WindowId, AssetReference>();

        public AssetReference RootCanvas =>
            _mainStaticData.WindowsData.rootCanvas;

        public IReadOnlyDictionary<string, AssetReference> SceneAssetReferences => 
            _mainStaticData.SceneReferences.ToDictionary<AssetReference>();

        public StaticDataService(MainStaticData mainStaticData)
        {
            _mainStaticData = mainStaticData;
        }
    }
}