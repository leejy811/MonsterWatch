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

        enemy.xDir = enemy.target.position.x > enemy.transform.position.x ? 1 : -1;
        enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * enemy.xDir, enemy.transform.localScale.y, enemy.transform.localScale.z);
        enemy.Move(enemy.chaseMoveSpeed);
    }
}
