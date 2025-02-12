using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    #region Components
    protected EnemyStateMachine stateMachine;
    protected Enemy enemy;
    #endregion

    protected float stateTimer;

    public EnemyState(Enemy _enemy, EnemyStateMachine _stateMachine)
    {
        enemy = _enemy;
        this.stateMachine = _stateMachine;
    }

    public virtual void Enter()
    {
    }

    public virtual void Update()
    {
        stateTimer += Time.deltaTime;
    }

    public virtual void Exit()
    {
        //player.anim.SetBool(animBoolName, false);
    }
}
