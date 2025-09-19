using UnityEngine;

public class Player_JumpState : Player_AiredState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float delayTimeToSlideState = 0.15f;
    public float timer;
    public Player_JumpState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(rb.linearVelocity.x, player.jumpForce);
        timer = delayTimeToSlideState;

    }
    public override void Update()
    {
        base.Update();
        timer -= Time.deltaTime;
        player.currentStateName = "Jump";
        if (rb.linearVelocity.y < 0f)
        {
            stateMachine.ChangeState(player.fallState);
        }
        if (player.wallDetected)
        {
            if (timer < 0)
            {
                stateMachine.ChangeState(player.wallSlideState);
                timer = delayTimeToSlideState;
            }
        }

    }
}
