using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class RabbitStateMachine
    {
        public RabbitState CurrentState { get; private set; }

        public void Initialize(RabbitState startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }

        public void ChangeState(RabbitState newState)
        {
            CurrentState.Exit();

            CurrentState = newState;
            newState.Enter();
        }
    }
}

