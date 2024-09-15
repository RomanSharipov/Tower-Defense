using System;

namespace Assets.Scripts.CoreGamePlay
{
    public class TurretStateMachine  
    {
        private ITurretState currentState;

        public void SetState(ITurretState newState, Action<ITurretState> setupBeforeEnter = null)
        {
            currentState?.Exit();
            currentState = newState;
            setupBeforeEnter?.Invoke(currentState);
            currentState.Enter();
        }

        public void Update()
        {
            currentState?.Update();
        }
    }
}
