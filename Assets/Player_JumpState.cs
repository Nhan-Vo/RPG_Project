using UnityEngine;

public class Player_JumpState : Player_AiredState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float delayTime = 0.15f;
    public Player_JumpState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(rb.linearVelocity.x, player.jumpForce);
        player.timer = delayTime;

    }
    public override void Update()
    {
        base.Update();
        player.timer -= Time.deltaTime;
        player.currentStateName = "Jump";
        if (rb.linearVelocity.y < 0f)
        {
            stateMachine.ChangeState(player.fallState);
        }
        if (player.wallDetected)
        {
            if (player.timer < 0)
            {
                stateMachine.ChangeState(player.wallSlideState);
                player.timer = 0.2f;
            }
        }

    }
}
