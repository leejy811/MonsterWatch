using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy _enemy, EnemyStateMachine _stateMachine) : base(_enemy, _stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (!enemy.CheckPlayer(enemy.attackRange))
        {
            stateMachine.ChangeState(enemy.chaseState);
            return;
        }

        if (stateTimer > enemy.attackCool)
        {
            stateTimer = 0.0f;
            enemy.Attack();
        }
    }
}
