using System.Collections.Generic;

namespace Assets.Scripts.CoreGamePlay
{
    public interface ITurretTransition
    {
        public bool ShouldTransition();
        public ITurretState TargetState { get; }
    }
}
