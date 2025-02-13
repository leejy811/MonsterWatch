using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public float attackTime;
    public BoxCollider2D boxCollider;

    public override void Attack()
    {
        base.Attack();
    }
}