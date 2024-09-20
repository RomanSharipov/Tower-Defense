using System;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class TurretStateMachine  
    {
        private ITurretState currentState;

        public void SetState(ITurretState newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }

        public void Update()
        {
            currentState?.Update();
        }
    }
}
