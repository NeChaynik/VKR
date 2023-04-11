using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class BearStateMachine
    {
        public BearState CurrentState { get; private set; }

        public void Initialize(BearState startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }

        public void ChangeState(BearState newState)
        {
            CurrentState.Exit();

            CurrentState = newState;
            newState.Enter();
        }
    }
}

