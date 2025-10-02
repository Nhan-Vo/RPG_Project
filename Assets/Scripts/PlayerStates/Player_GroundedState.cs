using UnityEngine;

public class Player_GroundedState : PlayerState //Whenever idle and move need to share some logic, ground sate is a super state to hold those logic
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Player_GroundedState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0f, rb.linearVelocity.y); 
    }

    public override void Update()
    {
        base.Update();
        if (rb.linearVelocity.y < 0 && player.groundDetected == false)
        {
            stateMachine.ChangeState(player.fallState);
        }

        if (input.Player.Jump.WasPerformedThisFrame())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        if (input.Player.Attack.WasPerformedThisFrame())
        {
            stateMachine.ChangeState(player.basicAttackState);
        }
    }
}

