using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class EnemyIdleState : EnemyState
{
    private float nextMoveTimer = 0.0f;

    public EnemyIdleState(Enemy _enemy, EnemyStateMachine _stateMachine) : base(_enemy, _stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        ResetTimer();
        ChangeDirection();

        enemy.target = null;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.CheckPlayer(enemy.chaseRange))
        {
            stateMachine.ChangeState(enemy.chaseState);
            return;
        }

        enemy.Move(enemy.idleMoveSpeed);
        RaycastHit2D rayHit = Physics2D.Raycast(enemy.transform.position + Vector3.right * enemy.xDir, Vector3.down, 1.25f, LayerMask.GetMask("Ground"));

        if (stateTimer > nextMoveTimer || rayHit.collider == null)
        {
            ResetTimer();
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        int prevDir = enemy.xDir;
        enemy.xDir = Random.Range(-1, 2);

        if (prevDir != enemy.xDir && enemy.xDir != 0)
            enemy.transform.localScale = new Vector3(enemy.xDir * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);
    }

    private void ResetTimer()
    {
        stateTimer = 0.0f;
        nextMoveTimer = Random.Range(1.0f, 3.0f);
    }
}
