using System.Collections.Generic;

namespace Assets.Scripts.CoreGamePlay
{
    public interface ITurretState
    {
        public void Enter();
        public void Update();
        public void Exit();
        public void AddTransitions(params ITurretTransition[] stateTransitions);
    }
}
