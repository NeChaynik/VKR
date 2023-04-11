using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class RabbitWalkState : RabbitState
    {
        float distanceToChangeGoal = 1.5f;
        public RabbitWalkState(Rabbit rabbit, RabbitStateMachine stateMachine) : base(rabbit, stateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();
            rabbit.agent.speed = 1.4f;
            rabbit.thisAnim.speed = 1.0f;
            rabbit.agent.destination = rabbit.ChooseGoal();
        }

        public override void HandleInput()
        {
            base.HandleInput();
            if (rabbit.hunt)
            {
                stateMachine.ChangeState(rabbit.hunted);
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (rabbit.agent.remainingDistance < distanceToChangeGoal)
            {
                rabbit.agent.destination = rabbit.ChooseGoal();
            } 
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}