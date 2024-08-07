using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TileId _tileId;
        
        public TileId TileId => _tileId;

        public int Q { get; private set; }
        public int R { get; private set; }
        
        public void Construct(int q,int r)
        {
            Q = q;
            R = r;
        }
    }
}
