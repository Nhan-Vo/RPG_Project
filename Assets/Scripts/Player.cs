using System.Collections;
using UnityEngine;

public class Player : Entity
{
    public PlayerInputSet input { get; private set; }

    public Player_IdleState idleState { get; private set; } // makes it so it doesn't appear in the inspector of Unity but other scripts can access it
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_WallSlideState wallSlideState { get; private set; }
    public Player_WallJumpState wallJumpState { get; private set; }
    public Player_DashState dashState { get; private set; }
    public Player_BasicAttackState basicAttackState { get; private set; }


    [Header("Attack details")]
    public Vector2[] attackVelocity;
    public float attackVelocityDuration = .1f;
    public float comboResetTime = 1f;
    private Coroutine queuedAttackCo;

    [Header("Movement details")]
    public string currentStateName; // For debugging purposes
    public float moveSpeed = 10f;
    public float jumpForce = 5f;
    public Vector2 wallJumpForce;
    public float dashCooldown;
    public float lastTimeDash { get; set; }
    [Range(0, 1)]
    public float wallSlideSlowMultiplier = .3f; // Should be between 0 and 1
    [Range(0, 1)]
    public float inAirMoveMultiplier = .7f; //Should be between 0 and 1
    [Space]
    public float dashDuration = .25f;
    public float dashSpeed = 20;
    public Vector2 moveInput { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        input = new PlayerInputSet();

        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        jumpState = new Player_JumpState(this, stateMachine, "jumpFall");
        fallState = new Player_FallState(this, stateMachine, "jumpFall");
        wallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide");
        wallJumpState = new Player_WallJumpState(this, stateMachine, "jumpFall");
        dashState = new Player_DashState(this, stateMachine, "dash");
        basicAttackState = new Player_BasicAttackState(this, stateMachine, "basicAttack");
    }
    private void OnEnable()
    {
        input.Enable();

        //input.Player.Movement.started - input just begun
        //input.Player.Movement.performed - input is performed
        //input.Player.Movement.canceled - input stops when you release the key

        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();// ctx = context
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable() // So when the player is dead, they cant jump or move
    {
        input.Disable();
    }

    public void EnterAttackStateWithDelay()
    {
        if (queuedAttackCo != null)
        {
            StopCoroutine(queuedAttackCo);
        }
        queuedAttackCo = StartCoroutine(EnterAttackStateWithDelayCo());
    }

    private IEnumerator EnterAttackStateWithDelayCo()
    {
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(basicAttackState);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
}
