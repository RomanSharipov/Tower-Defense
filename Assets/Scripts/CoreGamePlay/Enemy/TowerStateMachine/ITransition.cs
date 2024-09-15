using System.Collections.Generic;

namespace Assets.Scripts.CoreGamePlay
{
    public interface ITransition
    {
        public bool ShouldTransition();
        public ITurretState GetTargetState();
    }
}
