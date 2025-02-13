using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health")]
    public int curHP = 10;

    [Header("Move")]
    public float idleMoveSpeed = 1.0f;
    public float chaseMoveSpeed = 1.5f;
    public int xDir = 0;

    [Header("Attack")]
    public int damage;
    public float attackRange = 2.0f;
    public float attackCool = 2.0f;
    public float chaseRange = 3.0f;
    public Transform target;

    private bool isDie;

    #region Components
    protected Rigidbody2D rb;
    public Animator animator;
    #endregion

    #region States
    public EnemyStateMachine stateMachine { get; protected set; }

    public EnemyIdleState idleState { get; protected set; }
    public EnemyChaseState chaseState { get; protected set; }
    public EnemyAttackState attackState { get; protected set; }
    #endregion

    protected virtual void Awake()
    {
        //Components Initialize
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        //State Initialize
        stateMachine = new EnemyStateMachine(this);

        idleState = new EnemyIdleState(this, stateMachine);
        chaseState = new EnemyChaseState(this, stateMachine);
        attackState = new EnemyAttackState(this, stateMachine);

        stateMachine.Initialize(idleState);
    }

    void Update()
    {
        if (isDie) return;
        stateMachine.currentState.Update();
    }

    public void Move(float speed)
    {
        animator.SetBool("IsMoving", xDir != 0);
        transform.position += Vector3.right * xDir * speed * Time.deltaTime;
    }

    public virtual void Attack()
    {
        animator.SetTrigger("DoAttack");
        LookTarget();
    }

    public void LookTarget()
    {
        xDir = target.position.x > transform.position.x ? 1 : -1;
        transform.localScale = new Vector3(xDir * -1, transform.localScale.y, transform.localScale.z);
    }

    protected virtual IEnumerator PlayAttack()
    {
        yield return null;
    }

    public bool CheckPlayer(float radius)
    {
        RaycastHit2D rayHit = Physics2D.CircleCast(transform.position, radius, Vector2.up, 0.0f, LayerMask.GetMask("Player"));

        if (rayHit.collider != null)
        {
            target = rayHit.transform;
            return true;
        }
        else
            return false;
    }

    public Vector3 OnHit(int damage)
    {
        animator.SetTrigger("DoHit");

        OnDamage(damage);

        return transform.position;
    }

    public Vector3 OnHit(int damage, int hitTimes, float second)
    {
        animator.SetTrigger("DoHit");

        StartCoroutine(OnPoisionDamage(damage, hitTimes, second));

        return transform.position;
    }

    protected IEnumerator OnPoisionDamage(int damage, int hitTimes, float second)
    {
        for (int i = 0;i < hitTimes;i++)
        {
            if (isDie) break;
            OnDamage(damage);
            yield return new WaitForSeconds(second);
        }
    }

    protected void OnDamage(int damage)
    {
        curHP -= damage;

        if (curHP < 0)
            StartCoroutine(OnDie(1f));
    }

    protected virtual IEnumerator OnDie(float second)
    {
        isDie = true;
        gameObject.tag = "Untagged";

        animator.SetTrigger("DoDie");

        yield return new WaitForSeconds(second);

        Destroy(gameObject);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerController.instance.OnHit(transform.position, damage);
        }
    }
}
