using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine) : base(_player, _stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.jumpDuration;
        player.isJumping = true;
    }

    public override void Exit()
    {
        base.Exit();
        player.isJumping = false;
    }

    public override void Update()
    {
        base.Update();

        float yVel = player.jumpUpSpeed;
        if (stateTimer < 0.0f || !Input.GetKey(KeyCode.Space))
            yVel = player.jumpDownSpeed;

        float xVel = xInput * player.moveSpeed;
        player.SetVelocity(xVel, yVel);

        if (player.isGrounded && !Input.GetKey(KeyCode.Space))
            stateMachine.ChangeState(player.idleState);
    }
    
}
