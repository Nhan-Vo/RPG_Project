using UnityEngine;

public abstract class EntityState //This class is only a blueprint
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string animBoolName;

    protected Animator anim;
    protected Rigidbody2D rb;
    protected PlayerInputSet input;

    protected float stateTimer;

    public EntityState(Player player,StateMachine stateMachine, string stateName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = stateName;

        anim = player.anim;
        rb = player.rb;
        input = player.input;
    }

    public virtual void Enter()
    {
        // Everytime state will be changed, Enter() is called
        anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        // Run the logic of the state
        stateTimer -= Time.deltaTime;
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        if (input.Player.Dash.WasPressedThisFrame() && CanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
    }
    public virtual void Exit()
    {
        // Everytime we exit from a state and change to a new one, Exit() is called
        anim.SetBool(animBoolName, false);
    }
    private bool CanDash()
    {
        if (player.wallDetected)
            return false;
        if (stateMachine.currentState == player.dashState)
            return false;

        return true;
    }


}
