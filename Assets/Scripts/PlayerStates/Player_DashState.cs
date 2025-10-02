using UnityEngine;

public class Player_DashState : PlayerState
{
    private float originalGravityScale;
    private int dashDir;
    public Player_DashState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        // Declare the facing direction when dash to prevent changing direction mid-dash bug
        //dashDir = player.facingDir; <-- old code
        if (player.moveInput.x != 0)
            dashDir = ((int)player.moveInput.x);
        else
            dashDir = player.facingDir;
        
        stateTimer = player.dashDuration;

        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0f; // Disable gravity during dash

    }
    public override void Update()
    {
        base.Update();
        CancelDashIfNeeded();
        player.currentStateName = "Dash";
        player.SetVelocity(player.facingDir * player.dashSpeed, 0f);
        if (stateTimer < 0)
        {   
            if (player.groundDetected)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else if (!player.groundDetected)
            {
                stateMachine.ChangeState(player.fallState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.lastTimeDash = Time.time;
        player.SetVelocity(0, 0);
        rb.gravityScale = originalGravityScale; // Restore original gravity scale
    }
    private void CancelDashIfNeeded()
    {
        if (player.wallDetected)
        {
            if (player.groundDetected)
                stateMachine.ChangeState(player.idleState);
            else
                stateMachine.ChangeState(player.wallSlideState);
        }
        
    }
    
    
}
