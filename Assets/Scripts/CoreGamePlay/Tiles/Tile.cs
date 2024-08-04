using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TileId _tileId;

        public TileId TileId => _tileId;
    }
}
