using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
    public class LayerMaskProvider : ILayerMaskProvider
    {
        private readonly LayerMask _blockbuilding;
        private readonly LayerMask _tile;
        private readonly LayerMask _turret;
        private readonly LayerMask _groundEnemy;

        public LayerMask Blockbuilding => _blockbuilding;
        public LayerMask Tile => _tile;
        public LayerMask Turret => _turret;
        public LayerMask GroundEnemy => _groundEnemy;
        
        public LayerMaskProvider(LayerMask blockbuilding, LayerMask tile, LayerMask turret, LayerMask groundEnemy)
        {
            _blockbuilding = blockbuilding;
            _tile = tile;
            _turret = turret;
            _groundEnemy = groundEnemy;
        }
    }
}
