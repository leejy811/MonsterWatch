using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    int jumpDir = 1;
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine) : base(_player, _stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.jumpDuration;
        player.isJumping = true;
        jumpDir = 1;
    }

    public override void Exit()
    {
        base.Exit();
        player.isJumping = false;
    }

    public override void Update()
    {
        base.Update();

        //if (stateTimer > 0)
        //    jumpDir = 1;

        if (stateTimer < 0.0f || Input.GetKeyUp(KeyCode.Space))
            jumpDir = -1;

        float xVel = xInput * player.moveSpeed;
        float yVel = player.jumpSpeed * jumpDir;
        player.SetVelocity(xVel, yVel);

        if (player.isGrounded)
            stateMachine.ChangeState(player.idleState);

    }
    
}
