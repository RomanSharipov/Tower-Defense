using CodeBase.Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "LayerMaskProviderInstaller",
    menuName = "Scriptable Installers/LayerMaskProviderInstaller")]
    
    public class LayerMaskProviderInstaller : AScriptableInstaller
    {
        [SerializeField] private LayerMask _blockbuilding;
        [SerializeField] private LayerMask _tile;
        [SerializeField] private LayerMask _turret;
        [SerializeField] private LayerMask _groundEnemy;

        public override void Install(IContainerBuilder builder)
        {
            builder.Register<LayerMaskProvider>(Lifetime.Singleton)
                .WithParameter("blockbuilding", _blockbuilding)
                .WithParameter("tile", _tile)
                .WithParameter("turret", _turret)
                .WithParameter("groundEnemy", _groundEnemy)
                .As<ILayerMaskProvider>();
        }
    }    
}
