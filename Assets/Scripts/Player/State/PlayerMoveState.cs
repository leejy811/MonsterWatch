using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine) : base(_player, _stateMachine)
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

        Vector2 dir = new Vector2(xInput, 0);
        dir.Normalize();

        player.SetVelocity(player.moveSpeed * dir);

        //StateChange
        if (xInput == 0)
            stateMachine.ChangeState(player.idleState);

        if (Input.GetKey(KeyCode.Space))
            stateMachine.ChangeState(player.jumpState);

    }
}