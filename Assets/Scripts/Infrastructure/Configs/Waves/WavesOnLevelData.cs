using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(fileName = "WavesOnLevelData", menuName = "StaticData/WavesOnLevelData")]
    public class WavesOnLevelData : ScriptableObject
    {
        public WaveData[] WaveDatas;
    }
}
