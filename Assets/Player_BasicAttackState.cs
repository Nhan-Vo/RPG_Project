using UnityEditor.Tilemaps;
using UnityEngine;

public class Player_BasicAttackState : EntityState
{
    private float attackVelocityTimer;
    private int comboIndex = 1; // Start from 1 because in the animator, the first attack is indexed as 1
    private int comboLimit = 3;

    private float lastTimeAttacked;

    public Player_BasicAttackState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        ResetComboIndexIfNeeded();

        anim.SetInteger("basicAttackIndex", comboIndex);
        ApplyAttackVelocity();

    }
    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;
        if (attackVelocityTimer < 0)
        { 
            player.SetVelocity(0, rb.linearVelocity.y); 
        }
    }

    public override void Exit()
    {
        base.Exit();
        comboIndex++;
        lastTimeAttacked = Time.time;
    }

    private void ApplyAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(player.attackVelocity.x * player.facingDir, player.attackVelocity.y);
    }
    private void ResetComboIndexIfNeeded()
    {
        if (Time.time > lastTimeAttacked + player.comboResetTime)
            comboIndex = 1;

        if (comboIndex > comboLimit)
            comboIndex = 1;
    }
}
