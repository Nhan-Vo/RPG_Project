using UnityEngine;

public class Enemy : Entity
{
    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;
    public Enemy_BattleState battleState;

    [Header("Movement details")]
    public float idleTime = 2f;
    public float moveSpeed = 1.4f;
    //[Range(0, 2)]
    //public float moveAnimSpeedMultiplier = 1;

    [Header("Player detection")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance = 10;

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + new Vector3(playerCheckDistance * facingDir, 0, 0));

    }   
}
