using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
    public class LayerMaskProvider : ILayerMaskProvider
    {
        private readonly LayerMask _blockbuilding;
        private readonly LayerMask _tile;
        private readonly LayerMask _turret;

        public LayerMask Blockbuilding => _blockbuilding;
        public LayerMask Tile => _tile;
        public LayerMask Turret => _turret;

        public LayerMaskProvider(LayerMask blockbuilding, LayerMask tile, LayerMask turret)
        {
            _blockbuilding = blockbuilding;
            _tile = tile;
            _turret = turret;
        }
    }
}
