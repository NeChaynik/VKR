using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public abstract class BearState
    {
        protected Bear bear;
        protected BearStateMachine stateMachine;

        protected BearState(Bear bear, BearStateMachine stateMachine)
        {
            this.bear = bear;
            this.stateMachine = stateMachine;
        }

        public virtual void Enter()
        {
            //DisplayOnUI(UIManager.Alignment.Left);
        }

        public virtual void HandleInput()
        {

        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void PhysicsUpdate()
        {

        }

        public virtual void Exit()
        {

        }
    }
}

