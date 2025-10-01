using UnityEngine;

public class Player_MoveState : Player_GroundedState
{
    public Player_MoveState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }
    public override void Update() //override: use the method of the base class and add custom logic
    {
        base.Update();
        player.currentStateName = "Move";
        if (player.moveInput.x == 0 || player.wallDetected)
        {
            //Debug.Log("Space key was pressed. Transition to Idle State");
            stateMachine.ChangeState(player.idleState);
        }
        player.SetVelocity(player.moveInput.x * player.moveSpeed, rb.linearVelocity.y);
    }
}
