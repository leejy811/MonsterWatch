using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance = null;

    public int curHP = 10;
    public float moveSpeed = 1.0f;

    //Jump
    public float jumpDuration = 0.5f;
    public bool isJumping = false;
    public int jumpCnt = 0;
    public float jumpSpeed = 5.0f;
    public bool isGrounded = true;
    #region Components
    public Rigidbody2D rb;
    #endregion

    #region States
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerDamagedState damagedState { get; private set; }
    #endregion

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        { 
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        //Components Initialize
        rb = GetComponent<Rigidbody2D>();

        //State Initialize
        stateMachine = new PlayerStateMachine(this);

        idleState = new PlayerIdleState(this, stateMachine);
        moveState = new PlayerMoveState(this, stateMachine);
        jumpState = new PlayerJumpState(this, stateMachine);
        attackState = new PlayerAttackState(this, stateMachine);
        damagedState = new PlayerDamagedState(this, stateMachine);

        stateMachine.Initialize(idleState);
    }

    public static Player Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.Update();

    }

    private void FixedUpdate()
    {
        Debug.DrawRay(this.transform.position, Vector3.down * 1.25f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(this.transform.position, Vector3.down, 1.25f, LayerMask.GetMask("Ground"));
        if (rayHit.collider != null &&  rayHit.collider.CompareTag("Ground"))
            isGrounded = true;
        else
            isGrounded = false;
    }
    public void SetVelocity(float xVel, float yVel)
    {
        rb.velocity = new Vector2(xVel, yVel);
    }

    public void SetVelocity(Vector2 vel)
    {
        rb.velocity = vel;
    }

    
}
