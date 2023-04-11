using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class RabbitEatState : RabbitState
    {
        public RabbitEatState(Rabbit rabbit, RabbitStateMachine stateMachine) : base(rabbit, stateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();
            rabbit.thisAnim.speed = 2.0f;
            rabbit.agent.speed = 0f;
            rabbit.thisAnim.SetInteger("State", 1);
            rabbit.hunt = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
            if (!rabbit.isDead)
            {
                stateMachine.ChangeState(rabbit.walking);
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Exit()
        {
            base.Exit();
            rabbit.thisAnim.SetInteger("State", 2);
        }
    }
}

