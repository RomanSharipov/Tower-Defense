using VContainer;
using UnityEngine;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.UI
{
    public class GameLoopWindow : WindowBase
    {
        [Inject] private IWavesService _wavesService;

        [ContextMenu("TryGetEnemy()")]
        public void TryGetEnemy()
        {
            Debug.Log($"_wavesService = {_wavesService}");
        }
    }
}
