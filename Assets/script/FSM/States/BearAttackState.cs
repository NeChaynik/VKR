using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class BearAttackState : BearState
    {
        public BearAttackState(Bear bear, BearStateMachine stateMachine) : base(bear, stateMachine)
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
            if (bear.rabbit.isDead)
            {
                stateMachine.ChangeState(bear.eating);
            }
            else if (!bear.thisAnim.GetCurrentAnimatorStateInfo(0).IsName("Bear_Attack2"))
            {
                bear.thisAnim.SetInteger("State", 4);
                stateMachine.ChangeState(bear.hunting);
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
        }
    }
}

