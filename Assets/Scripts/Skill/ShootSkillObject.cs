using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ShootSkillObject : MonoBehaviour
{
    public float lifeTime = 0.0f;
    public float lifeTimer;
    public float moveSpeed;

    public Rigidbody2D rb;
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    public void Initialize(Vector2 dir)
    {
        rb.velocity = dir * moveSpeed;
        lifeTimer = lifeTime;

    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer < 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
