using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public abstract class RabbitState
    {
        protected Rabbit rabbit;
        protected RabbitStateMachine stateMachine;

        protected RabbitState(Rabbit rabbit, RabbitStateMachine stateMachine)
        {
            this.rabbit = rabbit;
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

