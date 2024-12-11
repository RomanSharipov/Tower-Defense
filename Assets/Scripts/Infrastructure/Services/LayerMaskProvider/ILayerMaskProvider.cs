using System;
using Assets.Scripts.CoreGamePlay;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
    public interface ILayerMaskProvider
    {
        public LayerMask Blockbuilding { get; }
        public LayerMask Tile { get; }
        public LayerMask Turret { get; }
        public LayerMask GroundEnemy { get; }
    }
}
