using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance = null;

    public int curHP = 3;
    public int maxHP = 3;

    [Header("Move Info")]
    public float moveSpeed = 1.0f;
    public int facingDir = -1; //right: 1, left: -1

    [Header("Jump Info")]
    public float jumpSpeed = 5.0f;
    public float fallSpeed = 3.0f;
    public int jumpCnt = 0;

    [Header("Dash Info")]
    public float dashDist = 1.0f;
    public float dashDuration;
    public float dashInterval;
    public float dashCoeff;

    [Header("Attack Info")]
    public float attackInterval;
    public float attackRecoilDuration;
    public float attackRecoilForce;

    [Header("Damaged Info")]
    public float damagedRecoilDuration;
    public float damagedRecoilForce;

    [Header("State Info")]
    public bool isJumping;
    public bool isFalling;
    public bool isGrounded;
    public bool isAttackable;
    public bool isDashable;
    public bool isDashReset;   
    public bool isInputEnabled;
    public bool isPoison;
    public bool isInvincibility;

    [Header("Time Info")]
    public float invincibilityTimeScale;
    public float damagedTimeScale;
    public float timeSlowDuration;

    [Header("Camera")]
    [SerializeField] private GameObject cameraObject;
    private CameraFollowObject cameraFollowObject;
    private float fallSpeedYDampingChangeThreshold;

    #region Components
    public Rigidbody2D rb;
    public Collider2D damagedCol;
    public Collider2D attackCol;
    public Animator animator;
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
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraFollowObject = cameraObject.GetComponent<CameraFollowObject>();

        fallSpeedYDampingChangeThreshold = CameraManager.instance.fallSpeedYDampingChangeThreshole;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerState();
        if (isInputEnabled)
        {
            Move();
            JumpControl();
            FallControl();
            SprintControl();
            AttackControl();
        }

        if (rb.velocity.y < fallSpeedYDampingChangeThreshold && !CameraManager.instance.isLerpingYDamping && !CameraManager.instance.lerpedFromPlayerFalling)
            CameraManager.instance.LerpYDamping(true);

        if(rb.velocity.y >= 0.0f && !CameraManager.instance.isLerpingYDamping && CameraManager.instance.lerpedFromPlayerFalling)
        {
            CameraManager.instance.lerpedFromPlayerFalling = false;

            CameraManager.instance.LerpYDamping(false);
        }
    }

    private void FixedUpdate()
    {
        isGrounded = CheckGrounded();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            OnHit(collision.transform.position);
        }
    }

    private void UpdatePlayerState()
    {
        animator.SetBool("IsGrounded", isGrounded);

        float yVel = rb.velocity.y;
        animator.SetBool("IsFalling", yVel < 0);

        if (isGrounded && yVel == 0)
        {
            animator.SetBool("IsJump", false);
            animator.SetBool("IsDown", false);

            jumpCnt = 2;
            isDashable = true;
        }
    }

    private void JumpControl()
    {
        if (!Input.GetKeyDown(KeyCode.LeftAlt))
            return;
        else if (jumpCnt > 0)
            Jump();
    }

    private void FallControl()
    {
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            isFalling = true;
            Fall();
        }
        else
            isFalling = false;
    }

    private void SprintControl()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isDashable && isDashReset)
            Dash();
    }

    private void AttackControl()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && isAttackable)
            Attack();
    }

    private void Move()
    {
        // calculate movement
        float xInput = Input.GetAxisRaw("Horizontal");
        float xVel = xInput * moveSpeed;

        // set velocity
        Vector2 newVelocity;
        newVelocity.x = xVel;
        newVelocity.y = rb.velocity.y;
        rb.velocity = newVelocity;

        // the sprite turn
        if(xVel * facingDir < 0)
        {
            // flip player sprite
            float yRot;
            if (facingDir > 0)
                yRot = 0.0f;
            else
                yRot = 180.0f;
            cameraFollowObject.CallTurn();

            Vector3 rotator = new Vector3(transform.rotation.x, yRot, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            facingDir *= -1;
        }

        //set anim parameter
        if (isGrounded && Mathf.Abs(xVel) > 0.0f)
            animator.SetBool("IsWalk", true);
        else
            animator.SetBool("IsWalk", false);
    }

    private void Jump()
    {
        Vector2 newVelocity;
        newVelocity.x = rb.velocity.x;
        newVelocity.y = jumpSpeed;

        rb.velocity = newVelocity;

        animator.SetBool("IsJump", true);
        jumpCnt -= 1;
    }

    private void Fall()
    {
        Vector2 newVelocity;
        newVelocity.x = rb.velocity.x;

        if(rb.velocity.y > -fallSpeed)
            newVelocity.y = -fallSpeed;
        else
            newVelocity.y = rb.velocity.y;

        rb.velocity = newVelocity;
    }

    private void Dash()
    {
        // reject input during sprinting
        isInputEnabled = false;
        isDashable = false;
        isDashReset = false;

        Vector2 dashPos;
        dashPos.x = this.transform.position.x + facingDir * dashDist;
        dashPos.y = this.transform.position.y;

        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, dashPos - (Vector2)this.transform.position, dashDist, LayerMask.GetMask("Ground", "Enemy"));
        Debug.DrawRay(this.transform.position, (dashPos - (Vector2)this.transform.position).normalized * dashDist, Color.blue, 1.0f);
        if (hit)
        {
            float newDist = Vector2.Distance(hit.point, (Vector2)this.transform.position) * dashCoeff - damagedCol.bounds.size.x / 2.0f;
            dashPos.x = this.transform.position.x + facingDir * newDist;
        }
        transform.position = dashPos;

        //_animator.SetTrigger("IsSprint");
        StartCoroutine(DashCoroutine(dashDuration, dashInterval));
    }

    private IEnumerator DashCoroutine(float dashDelay, float dashInterval)
    {
        yield return new WaitForSeconds(dashDelay);
        isInputEnabled = true;
        isDashable = true;

        if (!isGrounded)
            yield return new WaitUntil(() => isGrounded);
        else
            yield return new WaitForSeconds(dashInterval);

        isDashReset = true;
    }

    private void Attack()
    {
        //_animator.SetTrigger("IsAttack");
        //attackForwardEffect.SetActive(true);

        StartCoroutine(AttackCoroutine(attackInterval));
    }

    private IEnumerator AttackCoroutine(float attackInterval)
    {
        Vector2 origin = this.transform.position;

        ContactFilter2D filter = new ContactFilter2D();
        filter.NoFilter();

        List<Collider2D> targets = new List<Collider2D>();
        Physics2D.OverlapCollider(attackCol, filter, targets);
        foreach (Collider2D target in targets)
        {
            if (target.CompareTag("Enemy"))
            {
                Debug.Log("Hit Enemy");
            }
        }

        if (targets.Count > 0)
            StartCoroutine(RecoilCoroutine(targets[0].transform.position, attackRecoilForce, attackRecoilDuration));

        // attack cool down
        isAttackable = false;
        yield return new WaitForSeconds(attackInterval);
        isAttackable = true;
    }

    void OnHit(Vector3 targetPos)
    {
        if (!isInvincibility)
        {

            curHP--;
        }

        if (curHP <= 0)
            Dead();
        else
        {
            StartCoroutine(RecoilCoroutine(targetPos, damagedRecoilForce, damagedRecoilDuration));
            StartCoroutine(TimeSlowCoroutine());
        }
    }

    void Dead()
    {
        
    }

    private IEnumerator RecoilCoroutine(Vector3 targetPos, float recoilForce, float recoilDuration)
    {
        isInputEnabled = false;
        Vector3 recoilDir = this.transform.position - targetPos;
        recoilDir.Normalize();
        rb.AddForce(recoilDir * recoilForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(recoilDuration);

        isInputEnabled = true;
    }

    private IEnumerator TimeSlowCoroutine()
    {
        if (isInvincibility)
            Time.timeScale = invincibilityTimeScale;
        else
            Time.timeScale = damagedTimeScale;
        Debug.Log(Time.timeScale);
        yield return new WaitForSeconds(timeSlowDuration);
        Time.timeScale = 1.0f;
    }

    bool CheckGrounded()
    {
        Debug.DrawRay(this.transform.position, Vector3.down * 1.25f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(this.transform.position, Vector3.down, 1.25f, LayerMask.GetMask("Ground"));
        if (rayHit.collider != null && rayHit.collider.CompareTag("Ground"))
            return true;
        else
            return false;
    }


}
