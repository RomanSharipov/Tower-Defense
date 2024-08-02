using System.Collections.Generic;
using CodeBase.Configs;
using CodeBase.Infrastructure.UI.Services;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services
{
    public class StaticDataService : IStaticDataService
    {
        private readonly MainStaticData _mainStaticData;

        public IReadOnlyDictionary<WindowType, AssetReference> Windows => 
            _mainStaticData.WindowsData.ToDictionary<WindowType, AssetReference>();

        public IReadOnlyDictionary<string, AssetReference> SceneAssetReferences => 
            _mainStaticData.SceneReferences.ToDictionary<string,AssetReference>();

        public StaticDataService(MainStaticData mainStaticData)
        {
            _mainStaticData = mainStaticData;
        }
    }
}