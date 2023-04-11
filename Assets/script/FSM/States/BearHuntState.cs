using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class BearHuntState : BearState
    {
        float distanceToAttack = 2.5f;
        public BearHuntState(Bear bear, BearStateMachine stateMachine) : base(bear, stateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();
            bear.agent.speed = 6f;
        }

        public override void HandleInput()
        {
            base.HandleInput();
            if (bear.agent.remainingDistance < distanceToAttack)
            {
                bear.thisAnim.SetInteger("State", 3);
                stateMachine.ChangeState(bear.attack);
            }
            else if (!bear.hunt)
            {
                stateMachine.ChangeState(bear.walking);
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            bear.agent.destination = bear.rabbit.gameObject.transform.position; 
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


