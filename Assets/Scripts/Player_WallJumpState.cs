using UnityEngine;

public class Player_WallJumpState : EntityState
{
    private float moveLockTimer;
    private const float moveLockDuration = 0.1f;

    public Player_WallJumpState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(player.wallJumpForce.x * -player.facingDir, player.wallJumpForce.y);
        moveLockTimer = moveLockDuration;
    }
    public override void Update()
    {
        base.Update();
        player.currentStateName = "WallJump";

        //if (player.moveInput.x != 0 && rb.linearVelocity.x <4)
        //{
        //    player.SetVelocity(player.moveInput.x * (player.moveSpeed * player.inAirMoveMultiplier), rb.linearVelocity.y);
        //}

        if (moveLockTimer > 0)
        {
            moveLockTimer -= Time.deltaTime;
        }
        else
        {
            // Allow movement after the lock duration
            if (player.moveInput.x != 0)
            {
                player.SetVelocity(player.moveInput.x * (player.moveSpeed * player.inAirMoveMultiplier), rb.linearVelocity.y);
            }
        }

        if (rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.fallState);
        }
        if (player.wallDetected)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }

}



