using System.Collections.Generic;
using CodeBase.Infrastructure.UI.Services;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services
{
    public interface IStaticDataService
    {
        public IReadOnlyDictionary<WindowType, AssetReference> Windows { get; }

        public IReadOnlyDictionary<string, AssetReference> SceneAssetReferences { get; }
    }
}