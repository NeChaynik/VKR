using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM 
{
    public class RabbitHuntState : RabbitState
    {
        float distanceToChangeGoal = 1.5f;
        public RabbitHuntState(Rabbit rabbit, RabbitStateMachine stateMachine) : base(rabbit, stateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();
            rabbit.thisAnim.speed = 2.0f;
            rabbit.agent.speed = 3f;
        }

        public override void HandleInput()
        {
            base.HandleInput();
            if (!rabbit.hunt)
            {
                stateMachine.ChangeState(rabbit.walking);
            }
            else if (rabbit.isDead)
            {
                stateMachine.ChangeState(rabbit.eated);
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

