using UnityEngine;

public class Player_IdleState : Player_GroundedState
{
    public Player_IdleState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {

    }

    public override void Update() //override: use the method of the base class and add custom logic
    {
        base.Update();
        player.currentStateName = "Idle";

        if (player.moveInput.x == player.facingDir && player.wallDetected)
            return;
       

        if (player.moveInput.x != 0)
        {
            stateMachine.ChangeState(player.moveState);
        }
        

    }
}
