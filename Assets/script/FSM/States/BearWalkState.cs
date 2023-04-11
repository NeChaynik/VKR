using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class BearWalkState : BearState
    {
        float distanceToChangeGoal = 1.5f;

        public BearWalkState(Bear bear, BearStateMachine stateMachine) : base(bear, stateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();
            bear.agent.speed = 1.3f;
            bear.agent.destination = bear.ChooseGoal();
            bear.thisAnim.Play("Base Layer.Bear_WalkForward", 0, 1f);
        }

        public override void HandleInput()
        {
            base.HandleInput();
            if (bear.hunt)
            {
                stateMachine.ChangeState(bear.hunting);
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (bear.agent.remainingDistance < distanceToChangeGoal)
            {
                bear.agent.destination = bear.ChooseGoal();
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Exit()
        {
            base.Exit();
            bear.thisAnim.SetInteger("State", 1);
        }
    }
}

