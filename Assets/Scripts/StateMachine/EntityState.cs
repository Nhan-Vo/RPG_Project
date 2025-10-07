using UnityEngine;
using UnityEngine.Windows;

public abstract class EntityState 
{
    protected StateMachine stateMachine;
    protected string animBoolName;

    protected Animator anim;
    protected Rigidbody2D rb;

    protected float stateTimer;
    protected bool triggerCalled;

    public EntityState(StateMachine stateMachine, string animBoolName) 
    {
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }
    public virtual void Enter()
    {
        // Everytime state will be changed, Enter() is called
        anim.SetBool(animBoolName, true);
        triggerCalled = false;
    }

    public virtual void Update()
    {
        // Run the logic of the state
        stateTimer -= Time.deltaTime;
    }
    public virtual void Exit()
    {
        // Everytime we exit from a state and change to a new one, Exit() is called
        anim.SetBool(animBoolName, false);
    }

    public void CallAnimationTrigger()
    {
        triggerCalled = true;
    }
}
