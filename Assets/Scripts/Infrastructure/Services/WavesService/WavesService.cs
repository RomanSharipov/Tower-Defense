using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeBase.Configs;

namespace Assets.Scripts.Infrastructure.Services
{
    public class WavesService : IWavesService
    {
        private WavesOnLevelData _wavesOnLevelData;

        public void SetCurrentData(WavesOnLevelData wavesOnLevelData)
        {
            _wavesOnLevelData = wavesOnLevelData;
        }
    }

    public interface IWavesService
    {
        public void SetCurrentData(WavesOnLevelData wavesOnLevelData);
    }
}
