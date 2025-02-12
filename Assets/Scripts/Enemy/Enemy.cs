using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int curHP = 10;
    public float moveSpeed = 1.0f;
    public int damage;

    #region Components
    public Rigidbody2D rb;
    #endregion

    #region States
    public EnemyStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    #endregion

    void Awake()
    {
        //Components Initialize
        rb = GetComponent<Rigidbody2D>();

        //State Initialize
        stateMachine = new EnemyStateMachine(this);

        idleState = new PlayerIdleState(this, stateMachine);

        stateMachine.Initialize(idleState);
    }

    void Update()
    {
        stateMachine.currentState.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag)
    }
}
