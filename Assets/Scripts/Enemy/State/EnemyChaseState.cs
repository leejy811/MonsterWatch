using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(Enemy _enemy, EnemyStateMachine _stateMachine) : base(_enemy, _stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.animator.SetBool("isMoving", true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.CheckPlayer(enemy.attackRange))
        {
            stateMachine.ChangeState(enemy.attackState);
            return;
        }
        else if (!enemy.CheckPlayer(enemy.chaseRange))
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }

        enemy.LookTarget();
        enemy.Move(enemy.chaseMoveSpeed);
    }
}
