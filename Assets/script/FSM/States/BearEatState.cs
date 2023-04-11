using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class BearEatState : BearState
    {
        public BearEatState(Bear bear, BearStateMachine stateMachine) : base(bear, stateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();
            bear.agent.speed = 0f;
        }

        public override void HandleInput()
        {
            base.HandleInput();
            if (bear.thisAnim.GetCurrentAnimatorStateInfo(0).IsName("Bear_WalkForward"))
            {
                stateMachine.ChangeState(bear.walking);
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
            bear.hunt = false;
        }
    }
}

